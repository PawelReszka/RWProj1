fluent(has_gun_hador).
fluent(has_gun_mietus).
fluent(alive).
fluent(walking).

action(shoot).
action(chown).
action(entice).

preserve(_,_,[]).

executor(hador).
executor(mietus).

sneg(has_gun_hador, not_has_gun_hador).
sneg(has_gun_mietus, not_has_gun_mietus).
sneg(alive, not_alive).
sneg(walking, not_walking).

sinertial(has_gun_mietus).
sinertial(has_gun_hador).
sinertial(alive).
sinertial(walking).

initially_after([],[],[]).
initially_after([],[],[has_gun_hador,not_has_gun_mietus,alive,walking]).

always([
          [not_has_gun_hador, has_gun_mietus],
          [not_has_gun_mietus,has_gun_hador]
        ]).
always([
        [not_walking],
        [alive]
    ]).


order(0, has_gun_hador).
order(1, has_gun_mietus).
order(2, alive).
order(3, walking).

state(state0, [has_gun_hador, not_has_gun_mietus, alive, walking]).
state(state1, [not_has_gun_hador, has_gun_mietus, alive, walking]).
state(state2, [has_gun_hador, not_has_gun_mietus, alive, not_walking]).
state(state3, [not_has_gun_hador, has_gun_mietus, alive, not_walking]).
state(state4, [has_gun_hador, not_has_gun_mietus, not_alive, not_walking]).
state(state5, [not_has_gun_hador, has_gun_mietus, not_alive, not_walking]).

causes(chown, mietus, [[has_gun_hador]],[[]]).
causes(chown, hador, [[has_gun_mietus]], [[]]).
causes(shoot, mietus, [[not_alive]], [[has_gun_mietus]]).

typically_causes(shoot, hador, [[not_alive]], [[has_gun_hador]]).

releases(shoot,mietus,[walking], [[has_gun_mietus]]).
releases(shoot,hador,[walking], [[has_gun_hador]]).
