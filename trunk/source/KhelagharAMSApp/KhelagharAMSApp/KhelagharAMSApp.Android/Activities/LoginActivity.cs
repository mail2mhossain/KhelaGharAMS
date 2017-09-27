using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KhelagharAMSApp.Services;

namespace KhelagharAMSApp.Droid.Activities
{
  [Activity(Label = "Khelaghar AMS App", NoHistory = true)]
  public class LoginActivity : Activity
  {
    private EditText TextUsername;
    private EditText TextPassword;
    private TextView TextErrorMessage;
    private RelativeLayout RelativeLayoutButton;

    private string authenticationUserName = "";
    private string authenticationPassword = "";

    LoginService loginService = new LoginService();
    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      SetContentView(Resource.Layout.LoginLayout);
      // Create your application here
      FindViews();
      HandleEvents();
    }
    private void FindViews()
    {
      TextUsername = FindViewById<EditText>(Resource.Id.TextUsername);
      TextPassword = FindViewById<EditText>(Resource.Id.TextPassword);
      TextErrorMessage = FindViewById<TextView>(Resource.Id.TextErrorMessage);
      RelativeLayoutButton = FindViewById<RelativeLayout>(Resource.Id.RelativeLayoutButton);
    }
    private void HandleEvents()
    {
      RelativeLayoutButton.Click += RelativeLayoutButton_Click;
    }
    private void RelativeLayoutButton_Click(object sender, EventArgs e)
    {
      try
      {
        authenticationUserName = TextUsername.Text;
        authenticationPassword = TextPassword.Text;

        string success = loginService.Login(TextUsername.Text, TextPassword.Text, authenticationUserName, authenticationPassword);
        if (success == "Login Success")
        {
          ProgressBar();
          //Store Credentials to Shared Preferrence
          var storeCredentials = Application.Context.GetSharedPreferences("SmartMeterCredentials", FileCreationMode.Private);
          var storeCredentialsEdit = storeCredentials.Edit();
          storeCredentialsEdit.PutString("Username", TextUsername.Text);
          storeCredentialsEdit.PutString("Password", TextPassword.Text);
          storeCredentialsEdit.PutString("AuthenticationUserName", authenticationUserName);
          storeCredentialsEdit.PutString("AuthenticationPassword", authenticationPassword);

          storeCredentialsEdit.Commit();

          var mainActivity = new Intent(this, typeof(MainActivity));
          StartActivity(mainActivity);
        }
        else
        {
          TextErrorMessage.Visibility = ViewStates.Visible;
        }
      }
      catch (Exception ex)
      {
        var builder = new AlertDialog.Builder(this)
          .SetTitle("Smart Meter")
          .SetMessage("Wrong User ID or Password !!")
          .SetCancelable(true);

        var dialog = builder.Create();
        dialog.Show();
      }
    }
    public override void OnBackPressed()
    {
      base.OnBackPressed();
      Finish();
    }
    private void ProgressBar()
    {
      ProgressDialog progressDialog = new ProgressDialog(this);
      progressDialog.SetCancelable(true);
      progressDialog.SetMessage("Logging in...");
      progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
      progressDialog.Progress = 0;
      progressDialog.Max = 100;
      progressDialog.Show();
    }
  }
}