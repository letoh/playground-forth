 ( poor man's TCO, for pforth only )

0 value lastrec
: let-rec: <mark to lastrec ; immediate
: tail-recurse postpone branch lastrec <resolve ; immediate


: fib ( n -- m )
  dup 1 > if 1- dup recurse swap 1- recurse + then ;

: fib_opt ( n -- n )
  1 0 rot
  let-rec:
    dup 1 = if 2drop else
    -rot over + swap rot 1- tail-recurse then
;

: fact ( n -- n )
  dup 0= if drop 1 else dup 1- recurse * then
;

: fact_opt ( n -- n )
  1 swap
  let-rec:
    dup 0<> if
    dup 1- -rot * swap tail-recurse
    then
  drop
;

: test ( xt xt n m -- )
  ?do 2dup i swap execute i rot execute 2dup u. u. =
    if ." pass" cr else ." failed at " i u. cr then
  loop 2drop ;

cr .( * test fib:) cr
' fib ' fib_opt 10 1 test

cr .( * test fact:) cr
' fact ' fact_opt 10 1 test

cr .( * performance test:) cr
40 fib_opt u. cr ( 0m0.001s )
40 fib     u. cr ( 0m12.519s, much slower than the optimized versoin )

cr .( * execution test:) cr
550 fact_opt u. cr ( overflow but safe )
550 fact     u. cr ( segfault )



