#pragma strict

var speed:float;
function Start () {

}

function Update () {

	transform.Translate(0,0,speed*Time.deltaTime);
	Destroy(gameObject,3);
}