# AudioManager
Hey gang! Here's the audio manager repository I talked about in my Git UPM Integration video! Full disclosure I heavily followed and borrowed the coding techniques of the [Renaissance Coders](https://www.youtube.com/channel/UCkUIs-k38aDaImZq2Fgsyjw) from there [Game Essentials Playlist](https://www.youtube.com/watch?v=qkKuGmGRF2k&list=PL4CCSwmU04MiFVUqeDUYQaRIebMs8_dHX) as I haven't made an Audio Manager in a while and wanted to do some research on how to create an extendable and easy to use system. I liked there's the best, so that's what I went with! Now, lemme explain what's going on, and how to use it:
## The Scripts
Okay so there are three scripts to this framework: AudioType, AudioController, and TestAudio.
### AudioType.cs
AudioType is a class that simply holds a collection of all of the keys that the AudioController uses to classify and search for specific audio files to play from a source. This'll make more sense when I explain the AudioController script but you can think of the AudioType as the specific name of a song.
### AudioController.cs
The reason you're all here! The main course! The headliner! This baby is (for now) 254 lines of pure audio managerial goodness! Well, there are some debug functions and constructors, but you get the point. This is where the magic happens. "But how, Andy?" I hear you cry out. Well lemme tell ya how:
[Uh, this video explains it pretty well](https://www.youtube.com/watch?v=3hsBFxrIgQI&list=PL4CCSwmU04MiFVUqeDUYQaRIebMs8_dHX&index=3)
But you don't have an hour and a half to learn the intricacies of this controller, so let's run over the important pieces.
#### AudioObject
A constructor that holds an AudioType and an AudioClip. Using the music example, this is the name of a specific song, and the song attached to it.
#### AudioTrack
Holds a source and a list of AudioObjects, you can think of each of these as their own separate library of songs, or an album.
#### AudioJob
This class holds the parameters that change what the Coroutine running the show should do when it is presented with an AudioObject.
#### AudioAction
Tells the Coroutine what general action it should take when presented with an AudioObject, can be easily extended and edited in the Coroutine
#### RunAudioJob
Alright so here's where the magic happens. Continuing our music analogy, this coroutine is the DJ. You give him a job, and he gets the job done. What's cool about this is all you need to do to extend the features of this controller is create either a new action or parameter for a job, then edit this coroutine to do account for that parameter. And this will also keep any updates I make to this controller all in one script! For example/inspiration, I've already implemented optional fade and delay parameters that allow for a more cinematic audio experience straight from the code.
### TestAudio.cs
It's a WYSIWYG gang, exists purely to test that referencing the AudioController and giving it jobs works.
## Implementation
So now that we know what our three little scripts do, we gotta use 'em! And here's how:
#### Step 1
Place AudioController.cs on a game object in scene.
#### Step 2
Create your AudioSources, each Track is going to need it's own AudioSource. Because this system still retains all the functionality of the individual audio sources, if you have objects that need to be heard in world space(diegetic), give an instance of each unique object its own AudioSource, but non-diegetic AudioSources can just be on children objects of the game object AudioController.cs is on.
#### Step 3
Populate your tracks, this is as simple as setting the number of tracks you have, then filling them up. This starts by giving it an AudioSource, then giving each sound you want as part of that Track an AudioObject (which is just the AudioType and the clip itself)
#### Step 4
Get to referencing! You've done all the work you need to do in the inspector, so unless you have new sounds/sources/types to add, you can now just reference AudioController.cs in your scripts and start using audio in your scenes!
## Tips and Tricks
#### There can be only one!
Keep in mind that an Audio Source can only play one clip at a time, so if you have one track trying to run multiple jobs, it will go with whatever job it gets last and will stop running any jobs that came before, so if you have one object that needs to make multiple sounds at the same time, you'll need to create a separate audio source for each type of sound you want to be played (ie, if you want hear a character's footsteps and speech at the same time, they need to come from two separate audio sources)
#### Check out the Test Scene!
There's not much in there, but there is a functioning example of the controller with mutiple tracks, types, and sources, so if you're stuck on a particular step and the documentation isn't making something clear, it may be worth comparing what you have to the test scene!
