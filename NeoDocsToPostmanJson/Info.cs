namespace NeoDocsToPostmanJson
{
    internal class Export
    {
        public Info Info { get; set; }

        public List<Item> Item { get; set; } = new List<Item>();
    }

    internal class Info
    {
        public string _postman_id = "9c0b66ec-bc1a-4b47-8858-9a82ed88cb9d";

        public string Name = "RpcServer";

        public string Schema = "https://schema.getpostman.com/json/collection/v2.1.0/collection.json";

        public string _exporter_id = "1641087";

        public string PowerBy = "NeoDocsToPostmanJson";

        public string GitHub = "https://github.com/chenzhitong/NeoDocsToPostmanJson";
    }

    internal class Item
    {
        public string Name { get; set; }

        public Request Request { get; set; } = new Request();

        public Array Response { get; set; } = Array.Empty<string>();
    }

    internal class Request
    {
        public string Method = "POST";

        public Array Header { get; set; } = Array.Empty<string>();

        public Body Body { get; set; } = new Body();

        public string Description { get; set; }
    }

    internal class Body
    {
        public string Mode = "raw";

        public string Raw = "raw";

        public Options Options { get; set; } = new Options();
    }

    internal class Options
    {
        public Raw Raw { get; set; } = new Raw();

    }
    internal class Raw
    {
        public string Language = "json";
    }
}
