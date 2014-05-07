.( *** example 1 ***  ) cr
 
: foo ( a b -- c d )
  \ a -> b -> (a+b, a-b)
  ." foo "
  2dup +
  -rot -
;
 
: bar ( a b -- c d )
  \ a -> b -> (c*d, c/d)
  ." bar "
  2dup *
  -rot /
;
 
: baz ( a b -- c d )
  foo bar
;
 
( eval   ) 5 3 baz
( output ) swap . . cr
 
 
 
.( *** example 2 ***  ) cr
 
: fork ( a -- b c )
  \ a -> (a+1, a-1)
  dup 1+ swap 1-
;
 
: join ( a b -- c )
  \ a -> b -> a+b
  +
;
 
: proc ( a -- b )
  fork foo baz join
  fork bar foo join
;
 
( eval   ) 3 proc
( output ) . cr

