Press D, F, or B to enter different states that a SpyCar is capable of and watch the text update to that state.

This uses a state machine similar to Unity's guidlines in the Level Up Your Code book

This also uses an event system using scriptable objects to send data with a custom editor script to invoke for testing. You may also send void events by sending an empty struct.

The event currently sits in the statemanager and invokes on state transitions. You may add a listener to any object you wish to react to the event.

There is commented out code showing some example animations and also how you would properly set a flying object in an MR scene using the Meta SDK. It would ensure that it is below the room ceiling height and also not rendering a depth texture on the ceiling so you can still see it flying around.
