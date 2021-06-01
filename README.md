# AudioManager
Hey gang! Here's the audio manager repository I talked about in my Git UPM Integration video! Now, my first version heavily borrowed the coding techniques of the [Renaissance Coders](https://www.youtube.com/channel/UCkUIs-k38aDaImZq2Fgsyjw) from there [Game Essentials Playlist](https://www.youtube.com/watch?v=qkKuGmGRF2k&list=PL4CCSwmU04MiFVUqeDUYQaRIebMs8_dHX), but now I rewrote it to be way simpler to extend and way easier to use. Now here's how to use it.

## How to Use
1. Attach the AudioManager.cs script to an object in your scene.
2. Set a default audio source (this is the audio source that will be used if you do not specify an audio source when you use one of the Audio Actions).
3. Add all of the AudioSources you have in the scene into the Sources List.
4. Create a list of AudioObjects in the Clips List. Each AudioObjects contains an audioclip and a name to reference it by using Audio Actions.
5. Now when you script you simply add a reference to the AudioManager and use the Audio Actions, referencing the clips to play by using their name or index.

## Audio Actions
There are currently three available actions (with plans to expand soon):

### Play
Plays whatever AudioObject you specify through whatever Audio Source you specify (or through the default audio source if you do not specify one).

### Stop
Same as above, but stops the AudioObject being played through the Source.

### Restart
Stops the current AudioObject being played through that Source, then plays whatever AudioObject is specified

## Additional Parameters
In addition to the Audio Actions performing the commands above, you can also set certain parameters using either bools or floats. The two parameters currently available control a fade in/out effect and a delay.

### Fade
If set to true, then the AudioObject will play or stop while using a Lerp to increase/decrease the volume of the Audio Source over a second.

### Delay
If set to a float, then the Action will being once the time has been passed in the Coroutine.

