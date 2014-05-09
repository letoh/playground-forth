
: foo
	." foo" cr ;

: foo
	foo
	." foo 2" cr ;

foo


: fib ( n -- m )
  dup 1 > if 1- dup recurse swap 1- recurse + then ;

: fact ( n -- n! )
  dup 1 > if dup 1- recurse * then ;


: test ( xt n m -- )
	?do i over execute . cr loop drop ;

' fib 10 1 test
' fact 10 1 test

bye

