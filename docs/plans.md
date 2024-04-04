# Endpoints

To implement the ActivityPub protocol, Pithee must provide a set of minimal endpoints that are used to interact with the server. The following is a list of the endpoints that are required to implement the ActivityPub protocol:

| Endpoint | Description | Content-type
| --- | --- | --- |
| `/.well-known/webfinger` | Discovery endpoint to find the user`s profile URL. | `application/jrd+json` |
| `/users/:username` | User profile representing an ActivityStreams Person object. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/users/:username/outbox` | Publish messages and activities to followers. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/users/:username/inbox` | Receive messages and activities from other users. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/users/:username/followers` | List of users who follow a particular user. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/users/:username/following` | List of users that a particular user is following. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/users/:username/likes` | Collection of "Like" activities related to the user`s content. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/users/:username/shares` | Collection of shares or boosts of the user`s content. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |

## Additional (future) endpoints

| Endpoint | Description | Content-type
| --- | --- | --- |
| `/about` | Information about the server and its capabilities. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/admin` | Admin interface for managing users and server settings. | `text/html` |
| `/local/feed` | Local feed of activities from users on the same server. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/fediverse/feed` | Aggregated feed of activities from followed users. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/fediverse/inbox` | Receive messages and activities from other servers. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/posts/:id` | Individual post or activity object. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/posts/search?q=:query` | Search for posts or content based on a query string. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/posts/:id/replies` | Collection of replies to a post or activity. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/posts/:id/likes` | Collection of "Like" activities related to a post or activity. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/users/:username/feed` | User-specific feed of activities from followed users. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/search?q=:query` | Search for users or content based on a query string. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/media/search?q=:query` | Search for media objects based on a query string. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/media/:id` | Individual media object such as an image or video. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/users/search?q=:query` | Search for users based on a query string. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/users/:username/settings` | User-specific settings and preferences. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/users/:username/messages` | Direct messages between users. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/users/:username/block` | Block or mute a user to prevent interactions. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/users/:username/media` | Collection of media files uploaded by the user. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |
| `/users/:username/favorites` | Collection of favorited content by the user. | `application/ld+json; profile="https://www.w3.org/ns/activitystreams"` |



## Additional considerations:

### Content Negotiation
Servers and clients communicate using JSON, but for ActivityPub, the content type should be application/ld+json; profile="https://www.w3.org/ns/activitystreams".

### Authentication and Authorization
Authentication and authorization is done with OAuth 2.0 or username/password.

### Security
HTTPS only.

## Advanced features (Future):

Collections for Grouping Content: Such as images, videos, or articles.

Search and Discovery: To find content and users across the federated network.

Moderation Tools: To manage content and interactions within your server.
