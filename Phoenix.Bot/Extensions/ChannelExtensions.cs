using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;

namespace Phoenix.Bot.Extensions
{
    public static class ChannelExtensions
    {
        public static class Facebook
        {
            public static class ChannelDataFactory
            {
                public static JObject Template(Template template)
                {
                    return JObject.FromObject( new { attachment = new { type = "template", payload = template } });
                }
            }

            public abstract class Template
            {
                public abstract string Type { get; }
            }

            [JsonObject]
            public class ButtonTemplate : Template
            {
                [JsonProperty("template_type")]
                public override string Type { get => "button"; }

                /// <summary>
                /// UTF-8-encoded text of up to 640 characters.
                /// Text will appear above the buttons.
                /// </summary>
                [JsonProperty("text")]
                [StringLength(640, ErrorMessage = "ButtonTemplate's text must contain less than 640 characters.")]
                public string Text { get; set; }

                /// <summary>
                /// Set of 1-3 buttons that appear as call-to-actions.
                /// </summary>
                [JsonProperty("buttons")]
                [MaxLength(3, ErrorMessage = "ButtonTemplate must contain up to 3 buttons.")]
                public Button[] Buttons { get; set; }

                public ButtonTemplate() { }

                public ButtonTemplate(string text, Button[] buttons)
                {
                    this.Text = text;
                    this.Buttons = buttons;
                }
            }

            [JsonObject]
            public class GenericTemplate : Template
            {
                private string imageAspectRatio = "horizontal";

                [JsonProperty("template_type")]
                public override string Type { get => "generic"; }

                /// <summary>
                /// Optional. The aspect ratio used to render images specified by element.image_url.
                /// Must be horizontal (1.91:1) or square (1:1).
                /// Defaults to horizontal.
                /// </summary>
                [JsonProperty("image_aspect_ratio")]
                public string ImageAspectRatio 
                {
                    get => imageAspectRatio;
                    set => imageAspectRatio = value == "horizontal" || value == "square" ? value : throw new FacebookException();
                }

                /// <summary>
                /// An array of element objects that describe instances of the generic template to be sent.
                /// Specifying multiple elements will send a horizontally scrollable carousel of templates.
                /// A maximum of 10 elements is supported.
                /// </summary>
                [JsonProperty("elements")]
                [MaxLength(10, ErrorMessage = "There must be up to 10 elements in a Generic Template Caraousel.")]
                public GenericElement[] Elements { get; set; }

                public GenericTemplate() { }

                public GenericTemplate(string imageAspectRatio, GenericElement[] elements)
                {
                    this.ImageAspectRatio = imageAspectRatio;
                    this.Elements = elements;
                }
            }

            [JsonObject]
            public class GenericElement
            {
                /// <summary>
                /// The title to display in the template. 80 character limit.
                /// </summary>
                [JsonProperty("title")]
                [StringLength(80, ErrorMessage = "Element's title must be no more than 80 characters.")]
                public string Title { get; set; }

                /// <summary>
                /// Optional. The subtitle to display in the template. 80 character limit.
                /// </summary>
                [JsonProperty("subtitle")]
                [StringLength(80, ErrorMessage = "Element's subtitle must be no more than 80 characters.")]
                public string Subtitle { get; set; }

                /// <summary>
                /// Optional. The URL of the image to display in the template.
                /// </summary>
                [JsonProperty("image_url")]
                [Url]
                public string ImageUrl { get; set; }

                /// <summary>
                /// Optional. The default action executed when the template is tapped.
                /// Accepts the same properties as URL button, except title.
                /// </summary>
                [JsonProperty("default_action")]
                public UrlButton DefaultAction { get; set; }

                /// <summary>
                /// Optional. An array of buttons to append to the template.
                /// A maximum of 3 buttons per element is supported.
                /// </summary>
                [JsonProperty("buttons")]
                [MaxLength(3, ErrorMessage = "A maximum of 3 buttons per element is supported.")]
                public Button[] Buttons { get; set; }

                public GenericElement() { }

                public GenericElement(string title, string subtitle = null, string imageUrl = null, UrlButton defaultAction = null, Button[] buttons = null)
                {
                    this.Title = title;
                    this.Subtitle = subtitle;
                    this.ImageUrl = imageUrl;
                    this.DefaultAction = defaultAction;
                    this.Buttons = buttons;
                }
            }

            [JsonObject]
            public abstract class Button
            {
                public abstract string Type { get; }

                /// <summary>
                /// Button title. 20 character limit.
                /// </summary>
                [JsonProperty("title")]
                [StringLength(20, ErrorMessage = "Button title must contain up to 20 characters.")]
                public string Title { get; set; }
            }

            [JsonObject]
            public class UrlButton : Button
            {
                private string webviewHeightRatio = "full";
                private string webviewShareButton = null;

                [JsonProperty("type")]
                public override string Type { get => "web_url"; }

                /// <summary>
                /// This URL is opened in a mobile browser when the button is tapped.
                /// Must use HTTPS protocol if messenger_extensions is true.
                /// </summary>
                [JsonProperty("url")]
                [Url]
                public string Url { get; set; }

                /// <summary>
                /// Optional. Height of the Webview.
                /// Valid values: compact, tall, full. Defaults to full.
                /// </summary>
                [JsonProperty("webview_height_ratio")]
                public string WebviewHeightRatio
                {
                    get => webviewHeightRatio;
                    set => webviewHeightRatio = value == "compact" || value == "tall" || value == "full" ? value : throw new FacebookException();
                }

                /// <summary>
                /// Optional. Must be true if using Messenger Extensions.
                /// </summary>
                [JsonProperty("messenger_extensions")]
                public bool MessengerExtensions { get; set; } = false;

                /// <summary>
                /// The URL to use on clients that don't support Messenger Extensions.
                /// If this is not defined, the url will be used as the fallback.
                /// It may only be specified if messenger_extensions is true.
                /// </summary>
                [JsonProperty("fallback_url")]
                public string FallbackUrl { get; set; }

                /// <summary>
                /// Optional. Set to hide to disable the share button in the Webview (for sensitive info).
                /// This does not affect any shares initiated by the developer using Extensions.
                /// </summary>
                [JsonProperty("webview_share_button")]
                public string WebviewShareButton 
                { 
                    get => webviewShareButton; 
                    set => webviewShareButton = value == null || value == "hide" ? value : throw new FacebookException(); 
                }

                public UrlButton() { }

                public UrlButton(string url,
                    string webviewHeightRatio = null,
                    bool messengerExtensions = false,
                    string fallback_Url = null,
                    string webviewShareButton = null)
                {
                    this.Url = url;
                    this.WebviewHeightRatio = webviewHeightRatio;
                    this.MessengerExtensions = messengerExtensions;
                    this.FallbackUrl = fallback_Url;
                    this.WebviewShareButton = webviewShareButton;
                }
            }

            [JsonObject]
            public class PostbackButton : Button
            {
                [JsonProperty("type")]
                public override string Type { get => "postback"; }

                /// <summary>
                /// This data will be sent back to your webhook. 1000 character limit.
                /// </summary>
                [JsonProperty("payload")]
                [StringLength(1000, ErrorMessage = "Payload cannot exceed 1000 characters in length.")]
                public string Payload { get; set; }

                public PostbackButton() { }

                public PostbackButton(string payload) 
                {
                    this.Payload = payload;
                }
            }

            public class FacebookException : Exception {
                public FacebookException() : base("There was a problem with Facebook's Channel Data.") { }

                public FacebookException(string message) : base(message) { }
            }
        }
    }
}
