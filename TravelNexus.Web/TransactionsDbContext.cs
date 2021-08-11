//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;

//namespace TravelNexus.Web
//{
//    public class TransactionsDbContext : DbContext, IDisposable
//    {
//        //static TransactionsDbContext()
//        //{
//        //    Database.SetInitializer<TransactionsDbContext>(new TransactionDbInit());
//        //}
//        public TransactionsDbContext()
//          : base("NexusDBTransactions")
//        {


//            //Database.Initialize(false);
//        }

       

//        //public static TransactionsDbContext Create()
//        //{
//        //    return new TransactionsDbContext();
//        //}


//        //public class TransactionDbInit //CreateDatabaseIfNotExists<TransactionsDbContext>
//        //{

//        //    //protected override void Seed(TransactionsDbContext context)
//        //    //{
//        //    //    PerformInitialSetup(context); base.Seed(context);
//        //    //}

//        //    private void PerformInitialSetup(TransactionsDbContext context)
//        //    {

//        //    }

//        //    //public override void InitializeDatabase(TransactionsDbContext context)
//        //    //{
//        //    //    base.InitializeDatabase(context);

//        //    //}

//        //}
//    }
//}