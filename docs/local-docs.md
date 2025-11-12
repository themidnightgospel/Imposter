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

## 4. Publish to GitHub Pages (CI)

The repository includes a GitHub Actions workflow that publishes the site to the `gh-pages` branch using `mike`:

- Workflow: `.github/workflows/docs.yml`
- Triggers: on pushes to `master` or manual dispatch

To publish via CI, push your changes to `master`. The workflow will:

- Install MkDocs + Material + mike from `docs/requirements.txt`
- Deploy as version `dev` and update the `latest` alias
- Push to the `gh-pages` branch on the repository

## 5. Publish Locally (optional)

You can publish a local build using `mike` if you have push access:

```powershell
python -m pip install --upgrade pip
pip install -r docs/requirements.txt

# From repo root
mike deploy --push --remote origin --branch gh-pages dev latest
mike set-default --push --remote origin --branch gh-pages latest
```

This updates the GitHub Pages site immediately with the `dev` version and sets `latest` as the default.
