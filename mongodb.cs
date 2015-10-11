using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.IO;

namespace MongoDB
{
    class Program
    {
        public static void _fillEmployee(MongoDatabase db)
        {
            if (db.CollectionExists("employee"))
            {
                MongoCollection<BsonDocument> values = db.GetCollection("employee");
                for (int i = 0; i < 5; i++)
                {
                    BsonDocument b = new BsonDocument { {"name", "Epm" + i}, 
                    {"email", string.Format("e{0}@mail.ru", i)},
                    {"created",DateTime.Now}};
                    values.Insert(b);
                }
            }
        }

        public static void _getEmployee(MongoDatabase db)
        {
            if (db.CollectionExists("employee"))
            {
                MongoCollection<BsonDocument> values = db.GetCollection("employee");
                Console.WriteLine("In employee:");
                foreach (var item in values.FindAll())
                {
                    foreach (var item2 in item.Names)
                    {
                        BsonElement el = item.GetElement(item2);
                        Console.WriteLine("{0} : {1}", item2, el.Value);
                    }
                }
            }
        }

        public static void _updateEmployee(MongoDatabase db)
        {
            if (db.CollectionExists("employee"))
            {
                MongoCollection<BsonDocument> values = db.GetCollection("employee");
                BsonDocument bs = values.FindOne();
                bs.Set("name", BsonValue.Create("Emp100"));
                bs.Set("email", BsonValue.Create("e100@mail.ru"));
                values.Save(bs);
            }
        }

        public static void _fillEmployeeObject(MongoDatabase db)
        {
            if (db.CollectionExists("employee"))
            {
                MongoCollection<Employee> values = db.GetCollection<Employee>("employee");
                for (int i = 0; i < 5; i++)
                {
                    Employee b = new Employee
                    {
                        Id = ObjectId.GenerateNewId(),
                                              Name = "Epm" + i*10,
                                              Email = string.Format("e{0}@mail.ru", i*10),
                                              Created = DateTime.Now};
                    values.Insert(b);
                }
            }
        }

        public static void _getEmployeeObject(MongoDatabase db)
        {
            if (db.CollectionExists("employee"))
            {
                MongoCollection<Employee> values = db.GetCollection<Employee>("employee");
                Console.WriteLine("Values:");
                foreach (Employee item in values.FindAll())
                {
                    Console.WriteLine("{0}:{1}:{2}:{3}", item.Id.ToString(),
                        item.Name.ToString(), item.Email.ToString(), item.Created.ToString());
                }
            }
        }

        public static void _updateEmployeeObject(MongoDatabase db)
        {
            if (db.CollectionExists("employee"))
            {
                MongoCollection<Employee> values = db.GetCollection<Employee>("employee");
                Employee emp = values.FindOne();
                emp.Name = "Emp1000";
                
                values.Save(emp);

            }
        }

        static void Main(string[] args)
        {
            //1
            /*try
            {
                string sConn = @"mongodb://localhost";
                MongoServer server = MongoServer.Create(sConn);
                Console.WriteLine("connecting...");
                server.Connect();
                Console.WriteLine("State: " + server.State.ToString());
                server.Disconnect();
                Console.WriteLine("disconnected!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

            //2
            /*try
            {
                string sConn = @"mongodb://localhost";
                string sDbName = "csharp";
                MongoClient client = new MongoClient(sConn);
                MongoServer server = client.GetServer();
                MongoDatabase db = server.GetDatabase(sDbName);
                Console.WriteLine("DB:" + db.Name);
                //db.Drop();//delete db

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

            //3
            /*try
            {
                string sConn = @"mongodb://localhost";
                MongoClient client = new MongoClient(sConn);
                MongoServer server = client.GetServer();
                List<string> dbs = new List<string>(server.GetDatabaseNames());
                foreach (var item in dbs)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

            //4
            /*try
            {
                string sConn = @"mongodb://localhost";
                string sDbName = "csharp";
                MongoClient client = new MongoClient(sConn);
                MongoServer server = client.GetServer();
                MongoDatabase db = server.GetDatabase(sDbName);
                Console.WriteLine("DB:" + db.Name);
                var res = db.CreateCollection("employee");
                if(res.Ok)
                    Console.WriteLine("Collection created!");
                else
                    throw new Exception("Error while creating collection! " + res.ErrorMessage);

                db.CreateCollection("bank");
                db.CreateCollection("department");
                db.CreateCollection("branch");

                foreach (var item in db.GetCollectionNames())
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

            //5
            /*try
            {
                string sConn = @"mongodb://localhost";
                string sDbName = "csharp";
                MongoClient client = new MongoClient(sConn);
                MongoServer server = client.GetServer();
                MongoDatabase db = server.GetDatabase(sDbName);
                _fillEmployee(db);
                _fillEmployeeObject(db);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

            //6
            /*try
            {
                string sConn = @"mongodb://localhost";
                string sDbName = "csharp";
                MongoClient client = new MongoClient(sConn);
                MongoServer server = client.GetServer();
                MongoDatabase db = server.GetDatabase(sDbName);
                _getEmployee(db);
                _getEmployeeObject(db);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

            //7
            /*try
            {
                string sConn = @"mongodb://localhost";
                string sDbName = "csharp";
                MongoClient client = new MongoClient(sConn);
                MongoServer server = client.GetServer();
                MongoDatabase db = server.GetDatabase(sDbName);
                //_updateEmployee(db);
                _updateEmployeeObject(db);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

            //8
            /*try
            {
                string sConn = @"mongodb://localhost";
                string sDbName = "csharp";
                MongoClient client = new MongoClient(sConn);
                MongoServer server = client.GetServer();
                MongoDatabase db = server.GetDatabase(sDbName);
                MongoCollection<BsonDocument> values = db.GetCollection("employee");
                var q = new QueryDocument("name", "Epm1");
                foreach (BsonDocument b in values.Find(q))
                {
                    Console.WriteLine(b.ToString());
                }

                var q2 = Query.EQ("name", BsonValue.Create("Epm40"));
                var r = values.Remove(q2);
                if(r.Ok)
                    Console.WriteLine("Sucessfully deleted!");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

            //9
            /*try
            {
                string sConn = @"mongodb://localhost";
                string sDbName = "csharp";
                MongoClient client = new MongoClient(sConn);
                MongoServer server = client.GetServer();
                MongoDatabase db = server.GetDatabase(sDbName);
                //MongoCollection<BsonDocument> values = db.GetCollection("sign");
                MongoGridFS g = new MongoGridFS(db);
                MongoGridFSFileInfo f1 = g.Upload(@"C:\Users\avolobuev\Desktop\ico\db2ic.ico");
                MongoGridFSFileInfo f2 = g.Upload(@"C:\Users\avolobuev\Desktop\ico\word.ico");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

            //10
            /*try
            {
                string sConn = @"mongodb://localhost";
                string sDbName = "csharp";
                MongoClient client = new MongoClient(sConn);
                MongoServer server = client.GetServer();
                MongoDatabase db = server.GetDatabase(sDbName);
                //MongoCollection<BsonDocument> values = db.GetCollection("sign");
                MongoGridFS g = new MongoGridFS(db);
                BsonDocument f1 = g.Files.FindOne();
                //Console.WriteLine(f1.GetElement(0).Value);
                g.Download(@"C:\mongodb\down-" + Path.GetFileName(f1.GetElement(1).Value.AsString), Query.EQ("_id", BsonValue.Create(f1.GetElement(0).Value.AsObjectId)));
                g.DeleteById(f1.GetElement(0).Value.AsObjectId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

            //11
            /*try
            {
                string sConn = @"mongodb://localhost";
                string sDbName = "csharp";
                MongoClient client = new MongoClient(sConn);
                MongoServer server = client.GetServer();
                MongoDatabase db = server.GetDatabase(sDbName);
                MongoCollection<BsonDocument> values = db.GetCollection("transaction");
                for (int i = 0; i < 3; i++)
                {
                    BsonDocument b = new BsonDocument { 
                        {"traid", ObjectId.GenerateNewId()},
                        {"name", "Transaction " + i},
                        {"created",DateTime.Now},
                        {"user", new BsonDocument{
                            {"name", "Customer " + i}, 
                            {"email", string.Format("cust{0}@mail.ru", i)}
                           }
                        }
                    };
                    values.Insert(b);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

            //12
            /*try
            {
                string sConn = @"mongodb://localhost";
                string sDbName = "csharp";
                MongoClient client = new MongoClient(sConn);
                MongoServer server = client.GetServer();
                MongoDatabase db = server.GetDatabase(sDbName);
                MongoCollection<BsonDocument> values = db.GetCollection("transaction");
                foreach (var row in values.FindAll())
                {
                    foreach (var col in row.Names)
                    {
                        Console.WriteLine("{0}:{1}", row.GetElement(col).Name, row.GetElement(col).Value);
                    }
                }

                BsonDocument b = values.FindOne(Query.EQ("user.name","Customer 0"));
                Console.WriteLine(b.ToString());
                Console.WriteLine();

                BsonDocument b2 = b["user"].AsBsonDocument;
                b2.Set("name", BsonValue.Create("update-customer"));
                b2.Set("email", BsonValue.Create("update-email"));
                b.SetElement(new BsonElement("user", b2));
                values.Save(b);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

            //13
            /*try
            {
                string sConn = @"mongodb://localhost";
                string sDbName = "csharp";
                MongoClient client = new MongoClient(sConn);
                MongoServer server = client.GetServer();
                MongoDatabase db = server.GetDatabase(sDbName);
                MongoCollection<BsonDocument> values = db.GetCollection("transaction");
                for (int i = 1; i <= 3; i++)
                {
                    BsonDocument b = new BsonDocument { 
                        {"traid", ObjectId.GenerateNewId()},
                        {"name", "Transaction " + i*10},
                        {"created", DateTime.Now}
                    };

                    BsonArray ba = new BsonArray();
                    for (int j = 0; j < 3; j++)
                    {
                        ba.Add(new BsonDocument {
                                {"productid", j + 100},
                                {"price", (new Random()).Next(100, 200)},
                                {"userid", j}
                            }
                        );
                    }
                    BsonElement el = new BsonElement("orders", ba);
                    b.Add(el);

                    values.Insert(b);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

            //14
            /*try
            {
                string sConn = @"mongodb://localhost";
                string sDbName = "csharp";
                MongoClient client = new MongoClient(sConn);
                MongoServer server = client.GetServer();
                MongoDatabase db = server.GetDatabase(sDbName);
                MongoCollection<BsonDocument> values = db.GetCollection("transaction");
                var q = Query.EQ("orders.productid", BsonValue.Create(100));
                var u = Update.Set("orders.$.price", BsonValue.Create(700));
                var o = new MongoUpdateOptions();
                o.Flags = UpdateFlags.Multi;
                values.Update(q, u, o);
                BsonValue b = values.FindOne(q);
                Console.WriteLine(b);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

            //15
            /*try
            {
                string sConn = @"mongodb://localhost";
                string sDbName = "csharp";
                MongoClient client = new MongoClient(sConn);
                MongoServer server = client.GetServer();
                MongoDatabase db = server.GetDatabase(sDbName);
                MongoCollection<Employee> values = db.GetCollection<Employee>("employee");
                var q = from i in values.AsQueryable<Employee>()
                        where i.Created < DateTime.Now
                        select i.Name;
                foreach (var item in q)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

            //16
            /*try
            {
                string sConn = @"mongodb://localhost";
                string sDbName = "mydb";
                MongoClient client = new MongoClient(sConn);
                MongoServer server = client.GetServer();
                MongoCredentials cred = new MongoCredentials("abc","1234");
                MongoDatabase db = server.GetDatabase(sDbName, cred);
                MongoCollection<BsonDocument> values = db.GetCollection("customer");
                foreach (var item in values.FindAll())
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/


            Console.ReadLine();
        }
    }

    public class Employee
    {
        [BsonElementAttribute("_id")]
        public ObjectId Id {set; get;}
        [BsonElementAttribute("name")]
        public string Name { set; get; }
        [BsonElementAttribute("email")]
        public string Email { set; get; }
        [BsonElementAttribute("created")]
        public DateTime Created { set; get; }
    }


}
