using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Microsoft.Toolkit.Uwp.Notifications;
using Competition.Models;
using System.Diagnostics;
using Competition.ViewModels;

namespace Competition.Services
{
    public class TileService
    {
        static public XmlDocument CreateFiles(Matches Item)
        {
            string title = Item.name;
            string matchEvent = Item.matchEvent;
            string startTime = Item.startTime; 
            TileContent content = new TileContent()
            {
                Visual = new TileVisual()
                {
                    TileSmall = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            BackgroundImage = new TileBackgroundImage()
                            {
                                Source = "Assets/pic_1.jpg",
                                HintOverlay = 60
                            },
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = title,
                                    HintStyle = AdaptiveTextStyle.Subtitle
                                },

                                new AdaptiveText()
                                {
                                    Text = matchEvent,
                                    HintStyle = AdaptiveTextStyle.CaptionSubtle
                                },

                                new AdaptiveText()
                                {
                                    Text = startTime,
                                    HintStyle = AdaptiveTextStyle.Subtitle
                                }
                            }
                        }
                    },

                    TileMedium = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            BackgroundImage = new TileBackgroundImage()
                            {
                                Source = "Assets/pic_1.jpg",
                                HintOverlay = 60
                            },
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = title,
                                    HintStyle = AdaptiveTextStyle.Subtitle
                                },

                                new AdaptiveText()
                                {
                                    Text = matchEvent,
                                    HintStyle = AdaptiveTextStyle.CaptionSubtle
                                },

                                new AdaptiveText()
                                {
                                    Text = startTime,
                                    HintStyle = AdaptiveTextStyle.Subtitle
                                }
                            }
                        }
                    },

                    TileWide = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            BackgroundImage = new TileBackgroundImage()
                            {
                                Source = "Assets/pic_1.jpg",
                                HintOverlay = 60
                            },
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = title,
                                    HintStyle = AdaptiveTextStyle.Subtitle
                                },

                                new AdaptiveText()
                                {
                                    Text = matchEvent,
                                    HintStyle = AdaptiveTextStyle.CaptionSubtle
                                },

                                new AdaptiveText()
                                {
                                    Text = startTime,
                                    HintStyle = AdaptiveTextStyle.Subtitle
                                }
                            }
                        }
                    },

                    TileLarge = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            BackgroundImage = new TileBackgroundImage()
                            {
                                Source = "Assets/pic_1.jpg",
                                HintOverlay = 60
                            },
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = title,
                                    HintStyle = AdaptiveTextStyle.Subtitle
                                },

                                new AdaptiveText()
                                {
                                    Text = matchEvent,
                                    HintStyle = AdaptiveTextStyle.CaptionSubtle
                                },

                                new AdaptiveText()
                                {
                                    Text = startTime,
                                    HintStyle = AdaptiveTextStyle.Subtitle
                                }
                            }
                        }
                    }
                }
            };
            XmlDocument xdox = content.GetXml();
            return content.GetXml();
        }

        //磁贴
        static public void UpdateTileItem()
        {
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);
            updater.Clear();
            if (MatchesVM.GetMatchesVM().AllMatches.Count==0)
                return;
            else
            {
                int count = 0;
                foreach (var match in MatchesVM.GetMatchesVM().AllMatches)
                {
                    ++count;
                    var xmlDoc = TileService.CreateFiles(match);
                    TileNotification notification = new TileNotification(xmlDoc);
                    updater.Update(notification);
                    if (count == 5) break;
                }
            }
        }
    }
}
