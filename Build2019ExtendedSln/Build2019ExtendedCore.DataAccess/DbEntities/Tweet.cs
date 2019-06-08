using System;
using System.Collections.Generic;

namespace Build2019ExtendedCore.DataAccess.DbEntities
{
    public partial class Tweet
    {
        public string TweetId { get; set; }
        public string TweetText { get; set; }
        public string TweetedBy { get; set; }
    }
}