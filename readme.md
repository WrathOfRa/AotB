# __Attack of the Beat__
___
##### Project Members: Sean McManes and Hung Nguyen
##### Advisor: Professor Chia Han
###### December 9, 2019




---

### __Project Description/Abstract__
___
The goal of this project is to create a game that is an enjoyable experience for the player by challenging them, creating an interesting game environment, and has game mechanics that have not been fully explored by other games.

The main aspect that would make this game stand out from others is the use of game music that would manipulate the gameplay and environment. This would make the player not only have to look for visual cues in the game, but also audio cues in the music in order to have an advantage and complete the game.

Current games typically use the game music to set the tone of the world/gameplay. For example, creating a suspenseful setting for a scary/horror game with low to no sound, or playing exciting music in an action filled game to get the player immersed in that game. Some games have adapted their gameplay to the music or vice versa, but very few have directly used the music in order to modify the game experience. The ones closest to doing this have stopped short and only do basic implementations, such as movement or attacks based on a constant beat of a generic song.

For years, we have been invested in the game industry and have had experience with several game genres. Most of the enjoyment we found in these games stem from new experiences that the game present to the player. These experiences include new and interesting game mechanics or new interpretations to prior game mechanics, interesting world design, natural game controls and visuals, and more.

Our approach to this problem would include the following:
* Creating a tool/library that can analyze the game music that is being played and output the data retrieved to the running game or store it to be used when needed
  * The data stored would most likely be the frequencies found at various times in the music
* Create a working game environment that has an interesting design, interactable objects or items for the player, non-playable characters, and more to fill this environment
* With the analyzed music and working game environment, then aspects of the game can be manipulated by the data found. Some of these aspects include:
  * Enemy/game actions, such as having enemy attacks based on the frequencies of the current music
    * The type of attacks, power, and other aspects can be affected by this as well
  * Game pace can be affected by the type of music being played, with slower song making the game pace slower and fast, high energy, songs would make the pace faster and perhaps make the game harder
  * Aspects of the world design could also be affected by the song, with lighting and room design changing with the music



---

### __User Stories__
___
* As a programmer, I want the code to be clean and adaptable so I can make quick and simple additions to the game.

* As a game player, I want to be challenged to use my skills in the game so I can feel satisfaction and get rewarded from the game.

* As a game designer, I want to make an interesting and unique experience so that the player will be engaged and feel excitement during their time in the game.

* As an artist, I want the design and artwork in the game to work together and fit the game style, so that the experience for the player is visually pleasing and can accurately depict the game.



---

### __Design Diagrams__
___
##### Level 0

![](/Design_Diagrams/Design_Diagram_D0.png "Level 0")

---
##### Level 1

![](/Design_Diagrams/Design_Diagram_D1.png "Level 1")

---
##### Level 2

![](/Design_Diagrams/Design_Diagram_D2.png "Level 2")



---

### __Diagram Description__
___
All of the diagrams shown above demonstrate the general flow of inputs and outputs of our system, the user input and output devices, and the user themselves.  At every iteration we increase the depth and description of each major group in this cycle to show what options each member has to be able to influence the system.  At the deepest iteration, we detail the internal cycles of updates and projected outputs of our system and also the general flow of information within our input and output devices, and our user’s thought process and possible execution.



---

### __Project Tasks__
___
* Design overall basis of game style and design (may be changed or expanded in the future) – Sean and Hung
  * Design objects and functions to run game program – Sean
  * Design objects and functions for displaying game – Sean
  * Design objects and functions for basic game objects – Sean and Hung
  * Research and design a proper method for storing game objects and assets – Hung
  * Design objects and functions for object collisions and interactions – Hung
  * Design objects and functions for handling game music and sound effects – Sean
  * Design objects and functions for displaying and handling interactions with various game menus – Hung
* Develop the main components of the game engine – Sean and Hung
  * Test these components – Sean and Hung
  * Refine these components – Sean and Hung
  * Test these components – Sean and Hung
* Design basic/sample assets for the game to use – Hung
* Design player model and animations – Sean
* Design game world object’s models and animations – Sean and Hung
* Test and refine game objects and assets – Sean and Hung
* Design objects and functions for analyzing game music – Sean
* Design objects and functions to use this data to output proper actions for game objects – Sean
* Test and refine game objects and assets based on the actions from the game music – Sean and Hung
* Design additional game objects and functions to meet originally planned game mechanics – Sean and Hung
  * Additional player items (weapons, armor, …) to increase gameplay/player variety
  * Additional game environments and game world objects
* Create product demos – Sean and Hung



---

### __Project Timeline__
___

![](/Homework/Assignment_6/Timeline.png "Timeline")



---

### __Effort Matrix__
___

![](/Homework/Assignment_6/Effort_Matrix.png "Effort Matrix")



---

### __PowerPoint Slideshow__
___

https://github.com/WrathOfRa/AotB/tree/master/Homework/Assignment_8/Project_Slides.pptx



---

### __Self-Assessment Essays__
___

__Sean McManes__

Attack of the Beat is a project dedicated to creating an entertaining, exciting, and immersive game experience for the player and puts an emphasis on the music in the game.  This music will manipulate several functions in the game environment so that the player must focus on not only the game itself but also the game music to gain an advantage.  This aspect of gaming has not been approached in most modern games.  These games typically use their music to fill the background noise of the game and do almost nothing more than create an atmosphere for the game.  The music doesn’t have a direct affect in the game experience.  Our project will attempt to bridge this disconnect and create a game experience driven off the music in the game itself.  

The experiences that we gained during our academic career will assist our project development immensely.  Considering I have not completed any co-op semesters so far, most of my experience comes from the courses that I have completed during my time at the University of Cincinnati and from my own experiences outside of the class.  Our project will involve the use of several class types, functions and established libraries in order to achieve a functioning game.  CS 2011 Introduction to Computer Science and CS 2028C Data Structures were the courses that established and expanded my knowledge of programming with C++ and the importance of object designing in a program.  This project will require the use of user inputs, contain several game objects that will have to be managed and manipulated, and contain a form of storage for object information and other important data.  The skills gained from these courses will especially assist with the overall designing of object types, deciding what data needs to be stored and where, and creating the necessary functions to manipulate this data and interact with other objects.

As mentioned, the project will have to manage various objects in the game environment, store portions of this data for future use, and be able to read stored data as well.  This will allow the code of the project itself to stay clean of repetitive data that could be stored elsewhere and read from/written to when needed.  My experience from CS 2071 Discrete Structures, CS 4071 Design and Analysis of Algorithms, and CS 4092 Database Design and Development will assist me with these aspects of the project.  The objects in the game could vary depending on the scenario.  In the worst case, there can be several objects that need to be handled depending on what is happening in the game.  In order to do this, the object must be managed in various ways and my experience in these courses will especially help in that design process.  I will be able to choose the best ways of storing objects and finding the ones that need to be manipulated based on what the search parameters are and be able to evaluate the code to know if there are perhaps better options that would be more efficient.  Outside of these courses, I have experimented with ways of collecting user inputs and being able to display objects and outputs beyond what have been done during my coursework (typically just command line).  For this project, the use of libraries such as SDL2 and SFML will be useful for displaying the game and both reading and outputting audio.

Most of my motivation for this project comes from my enjoyment and time spent with video games during my life.  The countless experiences that I have gained from them and the communities that they have connected me with have affected several aspects of my academic career path and other aspects of my life.  From these experiences, I want to be able to expand upon the aspects that I find the most enjoyable for not only myself, but also for a larger audience.  During my time with video games and outside of them, I also gained an appreciation for music.  Some games have great soundtracks that add to the atmosphere of the game and can get you truly immersed in the experience.  Others seem to fall short or don’t fully use the music to add to the gameplay itself.  From my experience with both game and music I can see the disconnect between them and this became the foundation for this project.

Our first steps in reaching our goal for this project are to build a working game and game design that will be the backbone for the rest of the project.  If the game itself is not interesting to the player or is not functioning properly, then the rest of the project will fall short.  Once the basic game framework is set, then the aspects of the game that we want to be manipulated by the music can be decided and implemented.  The major aspect we have in mind are enemy attack and movement.  In order to manipulate these aspects, the music playing must be analyzed and the data from this interpreted by the game into actions for the objects in the game.  These actions will most likely be based on the frequency of the sounds, and their duration, being played, but their effects in game may depend on the objects that we want to be affected.  For example, certain enemies may only attack when a range of frequencies are played, and their attack power or types may depend on duration of those frequencies.  After these aspects have been refined and are working properly, then we will be able to work on expanding the game’s objects further.  Some of these ideas will be dependent on the time available for them, but if possible, it would be nice to have more variety in the game world, such as different area types, enemy types, game objects, and so on.  Overall, we will know we are done once we have made the experience enjoyable for the player and incorporates our major ideas on what the game music can manipulate to make that experience unique for the player.  We could then get feedback from other game enthusiasts to know what aspects of the game succeeded and if there are any areas that could be improved.

---

__Hung Nguyen__

For our project, Sean and I are creating a game called “ Attack of the Beat”. The project is about implementing our coding skills whilst learning some new designing skills to create a functioning game. The game will be a top down hack and slash with enemies based off the music playing. We feel like this would be a good project to test how good our programming skills are and learn some designing in the process. We will be combining multiple libraries and have already set up a list of what to do. This project will show how we’ve really grown as programmers in the four years we have been at University of Cincinnati.

We've both taken a lot of programming courses, and we’ve been introduced to Python, C++, Java, etc. For this project we’ve both decided to use C++ as it is the most common and functional. The first two CS courses helped me get familiar with C++ and how to use it. Data Structures helped me get a grasp of object-oriented programming and programming languages got me acquainted with safe coding habit to prevent memory loss. Discrete structures also helped me with the mathematical aspects of the game, figuring out trajectory and bounce movements.

For my co-op experience, I had plenty of coding experience working at FPT Software. We do a lot of third-party coding for clients, so I had opportunity to look through a lot of different kinds of codes for different software. This has really broadened my views of coding and the possibilities of it.  I’ve also learned a lot of teamwork skills, punctuality and responsibility that I intend to bring into this project. I really got to test my coding ability and learn new skills from my supervisors.

I have always been interested in game design, whether it’s the intricate technical aspects of Game AI or the specifics of game design. That’s why I’m extremely motivated to do this project and get a working product. Our approach to the game is to do the general layers of the game first. Then we would work on the specifics to improve on the finer details of the game. We have made a roadmap of what we want to do in what specific order and by following it, we believe we can effectively accomplish our goal. 

Our expected result is a working game with actual physics. This included knockback from being hit, bouncing bullet movements and other mathematical game mechanics. At the same time, we expect to be able to utilize a C library to read soundwaves and from that create matching enemy types. We will evaluate our contributions during weekly meetings and self-evaluation. We are not sure when we will be done since the project can always see improvements in our eyes and we have high hopes for it. However, we will know when we have done a good job when we have a working product of the game that runs smoothly.



---

### __Professional Biographies__
___

# Sean McManes #
*102 Montana Drive*
*Lake Waynoka, OH 45171*
*mcmanesw@mail.uc.edu*

### Co-op or Other Experiences and Responsibilities: ###
* __Cashier, Customer Service, and Assistant Supervisor, Kroger, Mt. Orab, Ohio. (2 Years)__
    * Effective time management skills
    * Ability to execute tasks under changing and stressful circumstances
    * Exceptional interpersonal and problem-solving skills
    * Able to complete various goals independently and as part of a team
    * Assisting supervisors and supervising several co-workers to complete tasks
    * Troubleshooting both customer and company technical issues
* __No Co-op Semesters__

### Skills/Expertise Areas: ###
* __Programming:__ C++, Python, Java
* __Operating Systems:__ Windows, Android, iOS
* __Database Programming:__ SQL
* __Office Applications:__ MATLAB, Microsoft Office, Google Apps
* __Illustration and Photo Editing:__ Adobe Photoshop, CorelDRAW, Procreate
* __Other:__ Building and Troubleshooting Computer Systems and Software

### Areas of Interest: ###
* Software Development
* Game Design/Development
* Database Applications

### Type of Project Sought: ###
* Creating a game engine/game which implements tools for deep game elements such as:
    * Object collision detection (especially for projectiles)
    * User interfaces and menu implementations
    * Role-playing game (RPG) elements that would include characters/objects with several attributes
    * Saving and loading prior game states
    * Artificial intelligence for objects in the game to create challenging and interesting aspects to the game
    * Possibly a retro game design (pixel art for game objects) with more modernized gameplay

---

# Professional Biography
## Contact Information
  * Name: Hung Nguyen Duy
  * Email: nguyeh6@mail.uc.edu
  * Phone No.: 513-302-4596
### Co-op Work Experience
  * FPT Software - Software Engineering Intern
    * Company Cloud-based storage setup
    * Web-based coding according to client request
  * Onesight -IT Intern
    * Web crawling for location data through a password protected website.
#### Project Sought:
  * I'm interested in and intend to do a project related to automatic trading algorithms or game development.



---

### __Budget__
___

| Date | By | Description | Hours |
--- | --- | --- | ---
8/27/2019 | Sean | Class Meeting and Project Discussion | 1.00
8/27/2019 | Hung | Class Meeting and Project Discussion | 1.00
8/30/2019 | Sean | 	Professional Biography | 	2.00
8/30/2019 | Hung | 	Professional Biography | 	2.00
9/3/2019 | 	Sean | 	Class Meeting and Project Discussion | 	1.00
9/3/2019 | 	Hung | 	Class Meeting and Project Discussion | 	1.00
9/6/2019 | 	Sean | 	Project Description | 	2.00
9/6/2019 | 	Hung | 	Project Description | 	2.00
9/8/2019 | 	Sean | 	Discussion of Overall Game Ideas | 	1.00
9/8/2019 | 	Hung | 	Discussion of Overall Game Ideas | 	1.00
9/10/2019 | 	Sean | 	Class Meeting and Project Discussion | 	1.00
9/10/2019 | 	Hung | 	Class Meeting and Project Discussion | 	1.00
9/12/2019 | 	Sean | 	Capstone Assessment | 	2.00
9/13/2019 | 	Hung | 	Capstone Assessment | 	2.00
9/14/2019 | 	Sean |	Researching Game Design Ideas | 	2.00
9/15/2019 | 	Hung | 	Begin Early Concept Designs for Game | 	3.00
9/17/2019 | 	Sean | 	Class Meeting and Project Discussion | 	1.00
9/17/2019 | 	Hung | 	Class Meeting and Project Discussion | 	1.00
9/18/2019 | 	Sean | 	Researching and Experimentation with Game Display Design | 	4.00
9/18/2019 | 	Sean | 	User Stories | 	2.00
9/18/2019 | 	Hung | 	User Stories | 	2.00
9/20/2019 | 	Sean | 	Design Diagrams Discussion | 	2.00
9/20/2019 | 	Hung | 	Design Diagrams Discussion | 	2.00
9/21/2019 | 	Sean | 	Researching and Experimentation with Object Management | 	3.00
9/21/2019 | 	Hung | 	Finished Early Assets for Game Testing | 	4.00
9/22/2019 | 	Sean | 	Design Diagrams Finalizing | 	2.00
9/22/2019 | 	Hung | 	Design Diagrams Finalizing | 	2.00
9/24/2019 | 	Sean | 	Class Meeting and Project Discussion | 	1.00
9/24/2019 | 	Hung | 	Class Meeting and Project Discussion | 	1.00
9/27/2019 | 	Sean | 	Initial Task List Discussion | 	2.00
9/27/2019 | 	Hung | 	Initial Task List Discussion | 	2.00
10/1/2019 | 	Sean | 	Class Meeting and Project Discussion | 	1.00
10/1/2019 | 	Hung | 	Class Meeting and Project Discussion | 	1.00
10/3/2019 | 	Sean | 	Task List Finalizing | 	2.00
10/4/2019 | 	Hung | 	Task List Finalizing | 	2.00
10/5/2019 | 	Hung | 	Researching Audio Analysis Tools | 	3.00
10/8/2019 | 	Sean | 	Class Meeting and Project Discussion | 	1.00
10/8/2019 | 	Hung | 	Class Meeting and Project Discussion | 	1.00
10/9/2019 | 	Sean | 	Timeline  | 	2.00
10/9/2019 | 	Hung | 	Timeline  | 	2.00
10/11/2019 | 	Sean | 	Effort Matrix | 	2.00
10/11/2019 | 	Hung | 	Effort Matrix | 	2.00
10/12/2019 | 	Sean | 	Continuing the Design of Early Game Display Functions | 	4.00
10/15/2019 | 	Sean |	Class Meeting and Project Discussion | 	1.00
10/15/2019 | 	Hung | 	Class Meeting and Project Discussion | 	1.00
10/18/2019 | 	Sean | 	Experimenting with Functions to Analyze Game Music | 	1.00
10/18/2019 | 	Hung | 	Experimenting with Functions to Analyze Game Music | 	1.00
10/23/2019 | 	Sean | 	Slideshow Discussion | 	2.00
10/23/2019 | 	Hung | 	Slideshow Discussion | 	2.00
10/25/2019 | 	Sean | 	Brain Storming Possible Game Music Manipulations | 	3.00
10/25/2019 | 	Hung | 	Designing and Researching Game Models and Assets | 	2.00
10/30/2019 | 	Sean | 	Slideshow Finalization and Recording | 	3.00
10/30/2019 | 	Hung | 	Slideshow Finalization and Recording | 	3.00
11/6/2019 | 	Sean | 	Designing High Level Ideas to Implement the Music Manipulations | 	2.00
11/8/2019 | 	Hung | 	Researching and Experimenting with Game Object Storage | 	2.00
11/12/2019 | 	Sean | 	Presentation and Class Meeting | 	2.00
11/12/2019 | 	Hung | 	Presentation and Class Meeting | 	2.00
11/15/2019 | 	Sean | 	Continuing Game Object Design and Early Testing	 | 2.00
11/17/2019 | 	Hung | 	Continuing Game Asset Design | 	2.00
11/19/2019 | 	Sean | 	Peer Presentations and Class Meeting/Disscussion | 	2.00
11/19/2019 | 	Hung | 	Peer Presentations and Class Meeting/Disscussion | 	2.00
11/22/2019 | 	Hung | 	Continuing Design Ideas for Game Asset and Object Storage | 	2.00
11/26/2019 | 	Sean | 	Peer Presentations and Class Meeting/Disscussion | 	2.00
11/26/2019 | 	Hung | 	Peer Presentations and Class Meeting/Disscussion | 	2.00
12/4/2019 | 	Sean | 	Meeting with Advisor | 	0.50
12/4/2019 | 	Hung | 	Meeting with Advisor | 	0.50
 |  |  |
 |  |  | Group Totals | 	121.00
 |  |  | Group Bill ($75/Hr)	 | $9075.00



---

### __Appendix__
___

All project files, references, and project updates can be found on the Attack of the Beat Github Repository located at the following link.

https://github.com/WrathOfRa/CS_Senior_Design

