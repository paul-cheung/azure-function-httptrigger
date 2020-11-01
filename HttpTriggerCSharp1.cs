using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Bot.Schema;
using System.Collections.Generic;

namespace Company.Function
{
    public static class HttpTriggerCSharp1
    {
        [FunctionName("HttpTriggerCSharp1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            [FromBody]Activity activity)
        {
            Attachment attachment = null;
            if (activity.Text.Contains("hero", StringComparison.InvariantCultureIgnoreCase))
            {
                var card = CreateSampleHeroCard();
                attachment = new Attachment()
                {
                    ContentType = HeroCard.ContentType,
                    Content = card
                };
            }
            else if (activity.Text.Contains("thumbnail", StringComparison.InvariantCultureIgnoreCase))
            {
                var card = CreateSampleThumbnailCard();
                attachment = new Attachment()
                {
                    ContentType = ThumbnailCard.ContentType,
                    Content = card
                };
            }

            if (attachment != null)
            {
                return await Task.FromResult(new OkObjectResult(new Activity()
                {
                    Attachments = new List<Attachment>() { attachment }
                }));
            }

            return await Task.FromResult(new OkObjectResult(new Activity()
            {
                Text = "Try to type <b>hero</b> or <b>thumbnail</b>."
            }));
        }

        private static HeroCard CreateSampleHeroCard()
        {
            return new HeroCard()
            {
                Title = "Superhero",
                Subtitle = "An incredible hero",
                Text = "Microsoft Teams",
                Images = new List<CardImage>()
                {
                    new CardImage()
                    {
                        Url = "https://github.com/tony-xia/microsoft-teams-templates/raw/master/images/cbd_after_sunset.jpg"
                    }
                },
                Buttons = new List<CardAction>()
                {
                    new CardAction()
                    {
                        Type = "openUrl",
                        Title = "Visit",
                        Value = "http://www.microsoft.com"
                    }
                }
            };
        }

        private static ThumbnailCard CreateSampleThumbnailCard()
        {
            return new ThumbnailCard()
            {
                Title = "Teams Sample",
                Subtitle = "Outgoing Webhook sample",
                Images = new List<CardImage>()
                {
                    new CardImage()
                    {
                        Url = "https://github.com/tony-xia/microsoft-teams-templates/raw/master/images/steak.jpg"
                    }
                },
                Buttons = new List<CardAction>()
                {
                    new CardAction()
                    {
                        Type = "openUrl",
                        Title = "Visit",
                        Value = "http://www.bing.com"
                    }
                }
            };
        }
    }
}
