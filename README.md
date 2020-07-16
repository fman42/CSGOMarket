# CSGOMarket
The library for working with CSGO Market API

In the moment a library has only 2 methods('cause i did this for my project and may be i'll update in future)
But if you want use this then get all documentation:

```
AuthClient client = new AuthClient("your_secret_key"); // You can add your HttpClient as second parametr
ItemEndPoint items = new ItemEndPoint(client);

var item = await items.GetItemInfo("314141_0"); // first method
if (item != null)
{
    Console.WriteLine(item.HashName);
    var result = await items.BuyItem("314141_0", item.MinPrice, "user steam id") // or you can pass tradeUrl instance of steamId. And yes, this is second method
}

```
