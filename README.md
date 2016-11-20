# haptikitten


meeeeeeeeeeeeeeeeeeeeeeeeeow

Unity + Tanvas + Android


## WHAT THE HECK IS ALL THIS?

Ok let's explain the process.

Root folder = main Unity project
Java/atu (short for Android Tanvas Unity) is a project to override the default Unity activity and AndroidManifest.xml

1. Open the root folder in Unity
2. Open Java/atu (short for Android Tanvas Unity) in AndroidStudio
3. In Unity, build the project for Android, making sure to select "Google Android Project." Save this to the Export folder
4. In Android Studio, click File -> Import Project and point it to the folder that was exported in step 3. I usually save this to the Java/android-unity folder.
5. You should now have a project which you can deploy to the device


