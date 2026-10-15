import json
from pathlib import Path
from urllib.request import Request, urlopen

root = Path(__file__).resolve().parent.parent / 'My project'
descriptor = json.loads((root / 'Library/Pipeline/.unity-pipeline-port').read_text())
request = Request(
    f"http://127.0.0.1:{descriptor['port']}/api/job/cancel",
    data=json.dumps({'id': 'be9bb96b34bd43ec858dc506f64b36bb'}).encode(),
    headers={'Authorization': 'Bearer ' + descriptor['evalToken'], 'Content-Type': 'application/json'},
)
with urlopen(request, timeout=10) as response:
    print(response.read().decode())
