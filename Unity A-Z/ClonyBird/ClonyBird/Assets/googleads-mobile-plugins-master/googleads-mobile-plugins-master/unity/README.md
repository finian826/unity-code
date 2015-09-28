Google Mobile Ads Unity Plugin
==============================

The Google Mobile Ads Unity Plugin helps provides a way to
serve Google Mobile ads in a Unity project deployed as native Android or iOS
applications. Plugin features include:

* A single package with cross platform (Android/iOS) support
* Mock ad calls when running inside Unity editor
* Support for Banner Ads
* Custom banner sizes
* Banner ad events listeners
* AdRequest targeting methods
* A sample project to demonstrate plugin integration

Interstitial support coming soon.

The plugin contains a .unitypackage file for those that want to easily import
the plugin, as well as the source code for those that want to iterate on it.

Requirements
------------
* Unity 4.3 (untested on previous versions)
* An ad unit ID
* To deploy on Android:
  * Android SDK 3.2 or higher
  * [Google Play services](http://developer.android.com/google/play-services/index.html)
    4.0 or higher
* To deploy on iOS:
  * XCode 5.1 or above
  * [Google Mobile Ads SDK](https://developers.google.com/mobile-ads-sdk/download#downloadios)


Integrate the Plugin into your Game
-----------------------------------

1. Open your project in the Unity editor.
2. Navigate to **Assets -> Import Package -> Custom Package**.
3. Select the GoogleMobileAdsPlugin.unitypackage file.
4. Import all of the files for the plugins by selecting **Import**. Make sure
   to check for any conflicts with files.


Android Setup
-------------
If you already had an AndroidManifest.xml in Plugins/Android/, keep your current
version and [add the necessary activities and permissions](https://developers.google.com/mobile-ads-sdk/docs#play)
required by the Google Mobile Ads SDK.

Additional dependencies:

1. Add the google-play-services_lib folder,
   located at <android-sdk>/extras/google/google_play_services/libproject,
   into the Plugins/Android folder of your project.

iOS Setup
---------

No pre-build setup required.

Run the project
---------------

If you're running the **HelloWorld** sample project, you should be able to run
the project now.

To build and run on Android, click **File -> Build Settings**, select the
Android platform, then **Switch Platform**, then **Build and Run**.

To build and run on iOS, click **File -> Build Settings**, select the iOS
platform, then **Switch Platform**, then **Build**. This will export an
XCode project. You'll need to do the following before you can run it:

1. Add the Google Mobile Ads iOS SDK library.
2. Add the following frameworks if they aren't already part of the project:
   * AdSupport
   * AudioToolbox
   * AVFoundation
   * CoreGraphics
   * CoreTelephony
   * MessageUI
   * StoreKit
   * SystemConfiguration
3. Add the **-ObjC** linker flag to your **Other Linker Flags** in
   **Build Settings**.

See the [developer docs](https://developers.google.com/mobile-ads-sdk/docs/#ios)
for more information on integrating the Google Mobile Ads iOS library.

Google Mobile Ads Unity API
===========================

The remainder of this guide assumes you are now attempting to write your own
code to integrate Google Mobile Ads into your game.

Basic Banner Flow
-----------------
Here is the minimal code needed to create a banner.

    using GoogleMobileAds.Api;
    ...
    // Create a 320x50 banner at the top of the screen.
    BannerView bannerView = new BannerView(
            "YOUR_AD_UNIT_ID", AdSize.Banner, AdPosition.Top);
    // Create an empty ad request.
    AdRequest request = new AdRequest.Builder().Build();
    // Load the banner with the request.
    bannerView.LoadAd(request);

The _AdPosition_ enum specifies where to place the banner.

Custom Ad Sizes
---------------
In addition to constants on _AdSize_, you can also create a custom size:

    using GoogleMobileAds.Api;
    ...

    // Create a 250x250 banner.
    AdSize adSize = new AdSize(250, 250);
    BannerView bannerView = new BannerView(
            "YOUR_AD_UNIT_ID", adSize, AdPosition.Bottom);

Ad Request Targeting
--------------------
If you want to provide custom targeting to ad requests, add the targeting
options when building the request. This sample ad request shows what options
you have for targeting. You only need to use the options that make sense for
your application.

    using GoogleMobileAds.Api;
    ...

    AdRequest request = new AdRequest.Builder()
        .AddTestDevice(AdRequest.TestDeviceSimulator)
        .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
        .AddKeyword("unity")
        .SetGender(Gender.Male)
        .SetBirthday(new DateTime(1985, 1, 1))
        .TagForChildDirectedTreatment(true)
        .AddExtra("color_bg", "9B30FF") // Sets text ad background color.
        .Build();

Test Ads
--------
To request test ads, add your encrypted device ID when building the ad request.
This ID can only be found in the logs on both Android and iOS when running your
apps and making a request. Once you get your device ID, pass it to
_AddTestDevice_.

Let's pretend my hashed device ID is _0123456789ABCDEF0123456789ABCDEF_, and I
also want to get test ads on the simulator. Here is how to set up the request:

    using GoogleMobileAds.Api;
    ...

    AdRequest request = new AdRequest.Builder()
        .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
        .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")  // Test Device 1.
        .Build();

Ad Events
---------
_BannerView_ contains ad events that you can register for:

    using GoogleMobileAds.Api;
    ...

    BannerView bannerView = new BannerView(
                "YOUR_AD_UNIT_ID", AdSize.Banner, AdPosition.Top);
    // Called when an ad request has successfully loaded.
    bannerView.AdLoaded += HandleAdLoaded;
    // Called when an ad request failed to load.
    bannerView.AdFailedToLoad += HandleAdFailedToLoad;
    // Called when an ad is clicked.
    bannerView.AdOpened += HandleAdOpened;
    // Called when the user is about to return to the app after an ad click.
    bannerView.AdClosing += HandleAdClosing;
    // Called when the user returned from the app after an ad click.
    bannerView.AdClosed += HandleAdClosed;
    // Called when the ad click caused the user to leave the application.
    bannerView.AdLeftApplication += HandleAdLeftApplication;
    ...

    public void HandleAdLoaded()
    {
        print("HandleAdLoaded event received.");
        // Handle the ad loaded event.
    }

You only need to register for the events you care about.

Banner Lifecycle
----------------
By default, banners are visible. To temporarily hide a banner, call:

    bannerView.Hide();

To show it again, call:

    bannerView.Show();

When you are finished with a banner, make sure to destroy it before dropping
your reference to it:

    bannerView.Destroy();

This lets the plugin know you no longer need the object, and can do any
necessary cleanup on your behalf.

Additional Resources
====================
* [Developer documentation](https://developers.google.com/mobile-ads-sdk/docs)
* [Developer forum](https://groups.google.com/group/google-admob-ads-sdk)
* [Google Ads +Page](https://plus.google.com/+GoogleAdsDevelopers)

