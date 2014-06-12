fluent(hasa).
fluent(hasb).
fluent(lr).
fluent(lrt).
action(do).
action(buy).
executor(hador).
sneg(hasa,not_hasa).
sneg(hasb,not_hasb).
sneg(lr,not_lr).
sneg(lrt,not_lrt).
sinertial(hasa).
sinertial(hasb).
sinertial(lr).
sinertial(lrt).
order(0, hasa).
order(1, hasb).
order(2, lr).
order(3, lrt).
state(state0,[lr,not_hasa,not_hasb,not_lrt]).
state(state1,[hasa,lr,not_hasb,not_lrt]).
state(state2,[hasb,lr,not_hasa,not_lrt]).
state(state3,[hasa,hasb,lr,not_lrt]).
state(state4,[lr,lrt,not_hasa,not_hasb]).
state(state5,[hasa,lr,lrt,not_hasb]).
state(state6,[hasb,lr,lrt,not_hasa]).
state(state7,[hasa,hasb,lr,lrt]).
initially_after([],[],[not_hasA, not_hasB]).
always([[lr]]).
typically_causes(do, epsilon, [lrt], []).
causes(buy, hador,[[hasa],[hasb]], []).
