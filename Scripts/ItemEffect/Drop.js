#pragma strict

var mine:GameObject ;
var oil:GameObject ;
var glue:GameObject ;

function Start () {
	
}

function Update () {

	if(Input.GetKeyDown("2"))
	{
		Instantiate(mine, transform.position, transform.rotation);
	}
	if(Input.GetKeyDown("3"))
	{
		Instantiate(oil, transform.position, transform.rotation);
        
	}
	if(Input.GetKeyDown("4"))
	{
		Instantiate(glue, transform.position, transform.rotation);
	}
}