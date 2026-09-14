# Better Ports
This mod adds better functionality to Sailwind's ports as well as adding one custom island — Bottleneck Island. The capitals of each archipelago have been dredged.

Bottleneck Island can be found at 31.15 N, 3.28 E in the Emerald Archipelago. Famous for its saltworks, coral reefs, kelp forests, and relaxed vibes; come visit or spend a holiday!


## Installation:
Install BepInEx and add this mod to the plugins folder. Ensure that the .dll and asset files `betterports` and `betterportsscenery` are in a folder called `Better Ports` inside the plugin folder. Your structure should look like this:

```
plugins
  \ Better Ports
    \ BetterPorts.dll
    \ betterports
    \ betterports.manifest
    \ betterportsscenery
    \ betterportsscenery.manifest
```

## Important

Requires `CustomIslandAPI` found [here](https://github.com/winterspices/CustomIslandAPI/releases/latest)

Not compatible with the old version of this mod "Deep Ports". Ensure previous versions are removed — they might be under a different name

## Configuration

There is a config file located in `Sailwwind/BepInEx/config` called `com.winter.betterports`. There is a variable called `EnableDrydock`, if set to `True` a large dry dock will appear at Fort Aestrin. Disabled by default.
