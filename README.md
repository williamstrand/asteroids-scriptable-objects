# Futuregames Development tools in game projects

## How to use:

Open the editor. It's located under "Window/Game Editor".
Select an object tagged with "Player" to begin editing the ship settings. Changing parameters in the editor will directly change the ship settings ScriptableObject.
- "Throttle Power" is the speed the ship moves forward.
- "Rotation Power" is the speed the ship rotates left and right.
- "Laser Speed" is the speed of the lasers the ship shoots.

Select an object tagged with "Asteroid" to begin editing the asteroid settings. Just like with the ship, changing parameters in the editor will change the asteroid settings ScriptableObject.
- "Min Force" and "Max Force" is the minimum and maximum force that can be applied to the asteroid.
- "Min Size" and "Max Size" is the minimum and maximum size the asteroid will have.
- "Min Torque" and "Max Torque" is the minimum and maximum rotation speed the asteroid can have.
- "Damage" is the damage the player will take when colliding with an asteroid.
- "Colors" is all the colors the asteroid can have.

## Process and thoughts:

The goal of the assignment was to create a tool that can edit the asteroids and ship settings. At first all of these settings were located in different components and hard to access. 
Because of this I created two ScriptableObjects containing the different variables. I have worked with ScriptableObjects before so these weren't a problem. They are really useful for storing information that multiple objects need to access.
Next I created two different editors, one for the ship and one for the asteroids. 

Creating the visuals for these were simple when using the UIToolkit.
The functionality on the other hand was trickier. In my opinion the way to create a custom editor is very unintuitive. It doesn't really work like everything else in Unity does. To get a reference to something I had to specify a path
to the objects location in the project instead of just dragging and dropping it like with everything else. I also had to learn about SerializedObjects and SerializedProperties which is also not used in much else in Unity. Atleast nothing I have used before.
To be able to bind the VisualElements to the properties of the ScriptableObjects you have make them into SerializedObjects. At first I manually converted every property on the SerializedObjects to SerializedProperties and bound every single on to the VisualElements by hand.
Later I discovered the Bind method which uses the binding paths you can specify on the VisualElements in the UI Builder. This made the code a lot cleaner and made the process a lot quicker. At this point both of the editors were functioning as I wanted
and now all that was left was to combine the two. I made a new editor and got references to both of the editors UXML files, then by using the OnSelectionChange method I check the tag of the tag of the selected object and show the correct editor based on what is selected.
If an object without an editor is selected the opened editor will be blank.

Although the UIToolkit is pretty uninituitive I can also feel that it is powerful. When I got the hang of it, it was really easy to do what I wanted. There also isn't that many resources to learn how to use it. 
In the future however I think that with the proper documentation and tutorials it could become really easy to learn and use. The UI Builder is a big plus. I absolutely hate HTML and CSS which the UIToolkit is very similar to.
With the UI Builder however you can skip these entirely, but those who want to use them can and you will get the same result. I also think you can design the UI entirely in C# code aswell which is cool.
Since I didn't do anything too complicated I can't really say if it's a good system in the long run, but as far as I can tell it shouldn't be too hard to expand upon my simple editor with more advanced features.
