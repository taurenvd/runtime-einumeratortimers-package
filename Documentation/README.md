##Usage

1. Add namespace for coroutines usage:
using UnityUseful.IEnumeratorUtils;
or use wrappers
UnityUseful.CorotineInstructionsWrappers

2. Use wrappers via keyword "this" on any MonoBehaviour.

Function:
this.TimerV(3f,FunctionAfter);

Lambda expression:
this.TimerV(3f,()=>{});

Chain coroutines:

var first = Timer(3f, Func);
var second = Timer(3f, Func2);

this.ChainV(first, second);