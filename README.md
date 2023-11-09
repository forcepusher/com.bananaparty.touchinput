# com.bananaparty.touchinput  
  
Unity package. Fully cross-platform touch input.  
  
Make sure you have standalone [Git](https://git-scm.com/downloads) installed first. Reboot after installation.  
In Unity, open "Window" -> "Package Manager".  
Click the "+" sign on top left corner -> "Add package from git URL..."  
Paste this: `https://github.com/forcepusher/com.bananaparty.touchinput.git#1.0.0`  
See minimum required Unity version in the `package.json` file.  
  
### Key notes:  
1. Works properly in WebGL on Mobile devices.  
	- Touch.deltaPosition is still bugged in Unity WebGL.  
2. Swipe gesture detector.  
	- Essential core feature that should be built-in, yet I had to write my own.  
3. Utility script for rejecting touches that meant for the UI.  
	- So player character no longer attacks when clicking buttons, finally.
