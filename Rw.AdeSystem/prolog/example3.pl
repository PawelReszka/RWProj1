fluent(light_on).
fluent(switch1_on).
fluent(switch2_on).

action(turn1).
action(turn2).

executor(hador).
executor(mietus).

sneg(light_on, light_off).
sneg(switch1_on, switch1_off).
sneg(switch2_on, switch2_off).

sinertial(switch1_on).
sinertial(switch2_on).

initially_after([],[],[]).
initially_after([],[],[switch1_on, switch2_on, light_on]).

always([
             [light_on, switch1_on, switch2_on],
             [light_on, switch1_off, switch2_off],
             [light_off, switch1_off, switch2_on],
             [light_off, switch1_on, switch2_off]
        ]).

state(state0, [light_on, switch1_on, switch2_on]).
state(state1, [light_off, switch1_off, switch2_on]).
state(state2, [light_on, switch1_off, switch2_off]).
state(state3, [light_off, switch1_on, switch2_off]).

causes(turn1, hador, [[switch1_on]], [[switch1_off]]).
causes(turn1, hador, [[switch1_off]], [[switch1_on]]).
causes(turn2, hador, [[switch2_on]], [[switch2_off]]).
causes(turn2, mietus, [[switch2_off]], [[switch2_on]]).


typically_causes(turn1, mietus, [[switch1_on]], [[switch1_off]]).
typically_causes(turn1, mietus, [[switch1_off]], [[switch1_on]]).
typically_causes(turn2, hador, [[switch2_off]], [[switch2_on]]).
typically_causes(turn2, mietus, [[switch2_on]], [[switch2_off]]).

