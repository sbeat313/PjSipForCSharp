using pjsua2xamarin.pjsua2;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnitTestXamarin
{
    public partial class App : Application
    {
        class MyAccount : Account
        {
            public override void onIncomingCall(OnIncomingCallParam prm)
            {
                base.onIncomingCall(prm);
            }

            public override void onRegState(OnRegStateParam prm)
            {
                base.onRegState(prm);
            }
        };

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            Endpoint ep = new Endpoint();
            ep.libCreate();

            // Initialize endpoint
            EpConfig ep_cfg = new EpConfig();
            ep.libInit(ep_cfg);

            //Create SIP transport.Error handling sample is shown
            TransportConfig sipTpConfig = new TransportConfig();
            sipTpConfig.port = 5060;
            ep.transportCreate(pjsip_transport_type_e.PJSIP_TRANSPORT_UDP, sipTpConfig);

            //Start the library
            ep.libStart();

            AccountConfig acfg = new AccountConfig();
            acfg.idUri = "sip:05012345678@ipphone.plala.or.jp";
            acfg.regConfig.registrarUri = "sip:ipphone.plala.or.jp";
            AuthCredInfo cred = new AuthCredInfo("digest", "*", "user_name", 0, "secret");
            acfg.sipConfig.authCreds.Add(cred);

            // Create the account
            MyAccount acc = new MyAccount();
            acc.create(acfg);

            // Here we don't have anything else to do..
            //Thread.Sleep(10000);
            Console.ReadLine();

            acc.Dispose();
            ep.libDestroy();
            ep.Dispose();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
