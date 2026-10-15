var camera = UnityEngine.Camera.main;
var follow = camera.GetComponent<DustAndWhip.CameraFollow>();
if (follow) follow.enabled = false;
camera.orthographicSize = 6.5f;
camera.transform.position = new UnityEngine.Vector3(32, -.5f, -10);
return "Temporary terrain overview camera";
