var time : float;
function Start () {
	yield WaitForSeconds(time);
	particleEmitter.emit = false;
}