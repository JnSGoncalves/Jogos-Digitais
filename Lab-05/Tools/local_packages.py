import json
from pathlib import Path

root = Path(__file__).resolve().parent.parent
manifest = root / '.validation-project/Packages/manifest.json'
data = json.loads(manifest.read_text(encoding='utf-8-sig'))
packages = {}
for path in (root / 'My project/Library/PackageCache').iterdir():
    info = path / 'package.json'
    if info.exists():
        packages[json.loads(info.read_text(encoding='utf-8-sig'))['name']] = path
pending = list(data['dependencies'])
visited = set()
missing = []
while pending:
    name = pending.pop()
    if name in visited or name.startswith('com.unity.modules.'):
        continue
    visited.add(name)
    if name not in packages:
        missing.append(name)
        continue
    path = packages[name]
    data['dependencies'][name] = 'file:' + path.as_posix()
    pending.extend(json.loads((path / 'package.json').read_text(encoding='utf-8-sig')).get('dependencies', {}))
manifest.write_text(json.dumps(data, indent=2), encoding='utf-8')
print('Local packages:', len(visited), 'Missing:', missing)
