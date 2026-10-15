"""Run a command on this project's existing Unity Pipeline connection.

Examples: python Tools/unity_request.py editor_status
          python Tools/unity_request.py eval --code-file Tools/generate.cs --job
Does not print the connection's authentication token.
"""
import argparse
import json
from pathlib import Path
from urllib.request import Request, urlopen

parser = argparse.ArgumentParser()
parser.add_argument('command')
parser.add_argument('--params', default='{}')
parser.add_argument('--code-file')
parser.add_argument('--job', action='store_true')
parser.add_argument('--job-id')
args = parser.parse_args()
root = Path(__file__).resolve().parent.parent / 'My project'
descriptor = json.loads((root / 'Library/Pipeline/.unity-pipeline-port').read_text())
headers = {'Authorization': 'Bearer ' + descriptor['evalToken'], 'Content-Type': 'application/json'}
base = f"http://127.0.0.1:{descriptor['port']}"
if args.job_id:
    request = Request(base + '/api/job?id=' + args.job_id, headers=headers)
else:
    parameters = json.loads(args.params)
    if args.code_file:
        parameters.update(code=Path(args.code_file).read_text(encoding='utf-8'), timeout=300000)
    body = {'command': args.command, 'parameters': parameters, 'timeout': 300000, 'job': args.job}
    request = Request(base + '/api/exec', data=json.dumps(body).encode(), headers=headers)
with urlopen(request, timeout=60) as response:
    print(response.read().decode())
