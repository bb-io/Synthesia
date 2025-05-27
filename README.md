# Blackbird.io Synthesia

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

[Synthesia’s](https://www.synthesia.io/) API enables developers to programmatically generate high-quality AI videos—with customizable avatars and synthetic voices—directly from text scripts

## Create your API key

To use [Synthesia API](https://docs.synthesia.io/reference/introduction), start by creating your API key:

- Go in the upper right corner of the Synthesia application and click on your account.
- Select 'Integrations' (You need to have 'Creator' plan or above)
- Click on Add to add a new Synthesia API key.
- Copy this key thanks to the 3-dot button on the right side of your API key.

## Actions

### Video

- **Search videos** Searches videos
- **Get XLIFF content of video** Downloads XLIFF content file of video
- **Download video** Downloads video by its ID
- **Get video captions** Downloads video captions by it`s ID
- **Update video content from XLIFF** Updates video content from XLIFF file
- **Create video** Creates a new video
- **Create video from template** Creates a new video from template

## Events

- **On video completed** triggers on any video completed
- **On video failed** triggers on any video failed


### Rate limits

| API Tier (Plan)        | Write Operations (per endpoint)                  | Read Operations (per endpoint)     |
|------------------------|--------------------------------------------------|------------------------------------|
| Tier 1 (Enterprise)    | `120 req/min | 600 req/hour | 3,000 req/day`     | `120 req/min | 80k req/day`        |
| Tier 2 (Enterprise)    | `80 req/min | 400 req/hour | 2,000 req/day`      | `80 req/min | 40k req/day`         |
| Tier 3 (Creator)       | `60 req/min | 300 req/hour | 1,000 req/day`      | `60 req/min | 20k req/day`         |



## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
