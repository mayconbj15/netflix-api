namespace Netflix.API.Utils.Constants
{
    public static class ResponseDescriptions
    {
        public const string MovieAllOk = "A list of all movies";

        public const string EpisodieAllOk = "A list of all episodies";

        public const string SeasonAllOk = "A list of all seasons";

        public const string SerieAllOk = "A list of all series";

        public const string InternalServerError = "Internal server error";

        public const string CreateCreated = "The created object";

        public const string CreateBadRequest = "If the request body is invalid";

        public const string ReadOk = "The requested object";

        public const string ReadNoContent = "If the object not exists";

        public const string UpdateOk = "The updated object";

        public const string UpdateUnprocessableEntity = "If the object not exists to be updated";

        public const string DeleteOk = "The delete object";

        public const string DeleteUnprocessableEntity = "If the object not exists to be deleted";
    }
}