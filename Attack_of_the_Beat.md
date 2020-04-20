# Attack of the Beat
---
#### Members:
- Sean McManes (mcmanesw@mail.uc.edu)

#### Faculty:
- Chia Han

---

#### Description:

The goal of this project is to create a game that is an enjoyable experience for the player by challenging them, creating an interesting game environment, and has game mechanics that have not been fully explored by other games.

The main aspect that would make this game stand out from others is the use of game music that would manipulate the gameplay and environment.  This would make the player not only have to look for visual cues in the game, but also audio cues in the music in order to have an advantage and complete the game.

Current games typically use the game music to set the tone of the world/gameplay.  For example, creating a suspenseful setting for a scary/horror game with low to no sound, or playing exciting music in an action filled game to get the player immersed in that game.  Some games have adapted their gameplay to the music or vice versa, but very few have directly used the music in order to modify the game experience.  The ones closest to doing this have stopped short and only do basic implementations, such as movement or attacks based on a constant beat of a generic song.

For years, we have been invested in the game industry and have had experience with several game genres.  Most of the enjoyment we found in these games stem from new experiences that the game present to the player.  These experiences include new and interesting game mechanics or new interpretations to prior game mechanics, interesting world design, natural game controls and visuals, and more.

Our approach to this problem would include the following:
* Creating a tool/library that can analyze the game music that is being played and output the data retrieved to the running game or store it to be used when needed
  * The data stored would most likely be the frequencies found at various times in the music
* Create a working game environment that has an interesting design, interactable objects or items for the player, non-playable characters, and more to fill this environment
* With the analyzed music and working game environment, then aspects of the game can be manipulated by the data found.  Some of these aspects include:
  * Enemy/game actions, such as having enemy attacks based on the frequencies of the current music
    * The type of attacks, power, and other aspects can be affected by this as well
  * Game pace can be affected by the type of music being played, with slower song making the game pace slower and fast, high energy, songs would make the pace faster and perhaps make the game harder
  * Aspects of the world design could also be affected by the song, with lighting and room design changing with the music.
