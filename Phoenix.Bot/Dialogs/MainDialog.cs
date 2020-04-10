using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Phoenix.Bot.Dialogs.Student;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Bot.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string GiphyBaseUrl = "http://api.giphy.com/v1/gifs/search";
        
        public IConfiguration Configuration { get; }

        public MainDialog(IConfiguration configuration)
            : base(nameof(MainDialog))
        {
            Configuration = configuration;

            AddDialog(new StudentDialog());
            AddDialog(new TeacherDialog());
            AddDialog(new WaterfallDialog(nameof(MainDialog) + "_" + nameof(WaterfallDialog),
                new WaterfallStep[]
                {
                    GreetingStepAsync,
                    LoopStepAsync
                }));

            InitialDialogId = nameof(MainDialog) + "_" + nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> GreetingStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string giphyFullUrl = GiphyBaseUrl + $"?rating=g&q=hi&limit=10&offset={new Random().Next(10)}&api_key={Configuration["GiphyKey"]}";
            string response = await httpClient.GetAsync(giphyFullUrl).Result.Content.ReadAsStringAsync();
            string gifUrl = JObject.Parse(response)["data"].First["images"]["downsized"]["url"].ToString();

            var mess = MessageFactory.Attachment(new Microsoft.Bot.Schema.Attachment(contentType: "image/gif", contentUrl: gifUrl));
            await stepContext.Context.SendActivityAsync(mess);

            mess = MessageFactory.Text(stepContext.Context.Activity.Text == "--persistent-greeting--"
                ? "Καλωσόρισες στο Nuage! 😁" : "Γεια σου! 😊");
            await stepContext.Context.SendActivityAsync(mess);

            //TODO: Find user type
            string userType = "student";

            return userType switch
            {
                "student" => await stepContext.BeginDialogAsync(nameof(StudentDialog), null, cancellationToken),
                "teacher" => await stepContext.BeginDialogAsync(nameof(TeacherDialog), null, cancellationToken),
                // "parent" =>
                _ => new DialogTurnResult(DialogTurnStatus.Cancelled),
            };
        }

        private async Task<DialogTurnResult> LoopStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.ReplaceDialogAsync(nameof(MainDialog) + "_" + nameof(WaterfallDialog), null, cancellationToken);
        }

    }
}
