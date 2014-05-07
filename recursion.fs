
: foo
	." foo" cr ;

: foo
	foo
	." foo 2" cr ;

foo


: fib ( n -- m )
  dup 1 > if 1- dup recurse swap 1- recurse + then ;


: test ( n m -- )
	?do i fib . cr loop ;

10 1 test

bye

