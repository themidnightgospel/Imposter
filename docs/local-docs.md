# Running the Docs Locally

Follow these steps from the repository root (`C:\Users\bubac\source\repos\Imposter`).

## 1. (Optional) Create a Virtual Environment

```powershell
py -3 -m venv .venv
.\.venv\Scripts\Activate.ps1
```

On macOS/Linux:

```bash
python3 -m venv .venv
source .venv/bin/activate
```

## 2. Install Documentation Dependencies

```powershell
pip install -r docs/requirements.txt
```

This installs `mkdocs`, `mkdocs-material`, and `mike`.

## 3. Serve the Site

```powershell
python -m mkdocs serve
```

MkDocs will build the site and watch for changes. Browse to the URL it prints
(typically http://127.0.0.1:8000/) to view the docs. Press `Ctrl+C` to stop the server.
