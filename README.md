# Next Generation Hero

By Ilya Vaschillo

Date: 4/8/25

[https://manyak404.github.io/NextGenHero/](https://manyak404.github.io/NextGenHero/)

# Waypoints

*   Tags: I learned to use tags to identify what object a collision is occurring with. Waypoints, eggs, player, and planes all had their own tags.
*   To make fluid movements I used lerp. It allows for rounded turns and smoother movements for enemy planes.
*   Waypoints move instead of being deleted.
*   I set visibility to change on key click, and planes to ignore waypoint queue order and instead fly to random waypoint on key click.

# Progress bar

*   To make the progress bar I used a slider UI element and removed the handle. It made a good filling bar but seems like a backwards way of doing things.
*   The fill value was the time since last egg was fired/fire rate
*   UI canvas had to be set to “Scale with screen size” to work with webgl.
*   Teaches me to be resourceful with what I have when working in Unity. Not every idea has a direct button or toggle so I might have to manipulate an element outside of its intended use.

All in all, I didn’t learn very many new skills. The only new thing I learned was how to make a progress bar and it taught me a new outlook on resourceful use of unity objects. It took me around 4 hours to complete the program. The most challenging part was keeping track of what waypoint a plane should fly to next, but even that was not too complicated.
