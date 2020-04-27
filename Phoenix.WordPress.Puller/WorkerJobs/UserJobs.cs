using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;
using Phoenix.WordPress.Puller.Extensions;
using Phoenix.WordPress.Puller.Helpers;
using Phoenix.WordPress.Puller.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCL;

namespace Phoenix.WordPress.Puller.WorkerJobs
{
    internal static class UserJobs
    {
        public static async Task UpdateAsync(WordPressClient client, PhoenixContext phoenixContext)
        {
            var customUsers = await client.CustomRequest.GetAll<CustomUser>(Routes.Users, embed: false, useAuth: true);
            foreach (var wpUser in customUsers)
            {
                var aspNetUser = phoenixContext.AspNetUsers.Add(new AspNetUsers()
                {
                    CreatedAt = DateTime.UtcNow,
                    UserName = wpUser.UserName
                });

                var user = phoenixContext.User.Add(new User()
                {
                    AspNetUserId = aspNetUser.Entity.Id,
                    FirstName = wpUser.FirstName,
                    LastName = wpUser.LastName
                });

                var role = phoenixContext.AspNetRoles.Single(r => r.Type == wpUser.Acf.Role);

                var userRole = phoenixContext.AspNetUserRoles.Add(new AspNetUserRoles()
                {
                    UserId = user.Entity.AspNetUserId,
                    RoleId = role.Id
                });

                if ((Role)role.Type == Role.Student)
                {
                    foreach (var courseWpId in wpUser.Acf.CourseWpIds)
                    {
                        var studentCourse = phoenixContext.StudentCourse.Add(new StudentCourse()
                        {
                            CourseId = courseWpId,
                            StudentId = user.Entity.AspNetUserId
                        });
                    }
                }

                return;
            }
        }
    }
}
