# Movies Feed

Movies feed application with .NET backend and React frontend.

Downloads movies feed from an external API, serves this data to frontend where it is displayed. Caches images to make sure no extra downloads are made.

## Quick Start

```bash
docker-compose up -d
```

Access the app at http://localhost:3000


# Next Steps

While the current solution fits all requirements, there are some steps that I would like to emphasise on if I had to continue this project.

## Scalability

To make this backend solution scalable I would:
- use Redis for
  - keeping hash to url mapping
  - using distributed locks for concurrency control in a scalable way
- store cached images in a persistent way - either file storage with a shared persistent volume or in cloud file storage if available

## CI/CD

Add CI pipeline that would run tests.

Once decided where to host the solution - setup IaaS and a CD pipeline.