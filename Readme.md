# Magical Tower

## Known Architecture Improvement Points

1. Move View classes to an MVP architecture. Views already have a shared entry point through factories.
2. Add object pooling to factories.
3. Add a `GameStateMachine` to `GamePresenter`. A state machine would make the game lifecycle explicit: bootstrap, playing, win, lose, cleanup/restart.
4. Add cancellation for async flows through `CancellationToken`.