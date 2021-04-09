# Rock Paper Scissors
A client requests that you build a rock, paper, scissors console application game in C#.

[![License](https://img.shields.io/github/license/LucasRosinelli/rock-paper-scissors)](./LICENSE)
[![.NET Core](https://github.com/LucasRosinelli/rock-paper-scissors/workflows/.NET%20Core/badge.svg)](./.github/workflows/main.yml)

## Requirements
- The game must be playable by either two human players or against a computer player.
- The console application should clearly display the game’s progression and be easy to use.
- Focus on code quality.

## Rules
- The first player to win 3 rounds wins.
- During each round, players select one of the three following options:
  - **Rock**, which beats **Scissors**
  - **Paper**, which beats **Rock**
  - **Scissors**, which beats **Paper**
- When the same option is selected by both players, the round is restarted.

## Computer player behavior
- The computer player chooses the option that would have beat its previous selection.
  - Ex: In round _1_, the computer chooses **Rock**, then in round _2_ it selects **Paper**.

## Design
Your design should make it easy to implement these upcoming requirements.

## Additional features
- Adding new options. For example, I want to add a **Flamethrower** option that beats **Paper** and loses to **Rock** and **Scissors**.
- We want to add a new **computer player type** which always selects a random option.

## Author
[Lucas Rosinelli](https://lucasrosinelli.com/)
