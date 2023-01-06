# Bulletin Board

A simple grid board that allows a user to drag and drop objects and attach them to the board.
Objects cannot overlap, and cannot be hanging off the board.

[![Bulletin Board Video](https://img.youtube.com/vi/H3higT8RCPc/0.jpg)](https://youtu.be/H3higT8RCPc)

## How it works

A Flexalon Grid Layout component is positioned in front of the board.
A custom Bulletin Board script fills the grid with empty gameObjects.

Each rectangle item has these important components:
 - Bulletin Board Item - tells the Bulletin Board how many cells this item occupies.
 - Flexalon Interactable - allows the object to be dragged.
 - Flexalon Constraint - allows the object to be attached to the board.
 - Flexalon Lerp Animator - smooths the motion of the item when it attaches to the board.

When the user drags and releases a rectangle, the Bulletin Board script finds the nearest child in the grid
which is not occupied by another item. If it finds a valid nearest child, it
uses the Flexalon Constraint component to attach the item to the child.