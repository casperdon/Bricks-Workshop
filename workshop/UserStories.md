# User Stories for the Notification Service Library Workshop
This File contains the User Stories for the Workshop. Each story will extend the previous one, adding new requirements and constraints. The goal is to simulate the real-world challenges of designing a reusable library that must satisfy multiple consumer needs simultaneously.

# User Story 1 — Basic Integration
As a User i want to be able to inject the Notification Service into my application using dependency injection and configure the way it behaves. I Want to be able to specify the API key and base URL for the Notification Service in my appsettings.json file. I also want to be able to configure the underlying HttpClient used by the Notification Service, for example to set timeouts or add custom handlers.

-- Developer Notes:
- The library should provide an extension method for `IServiceCollection` that registers the necessary services. This can be added in the provided `ServiceCollectionExtensions.cs` file.
- The library should use `IHttpClientFactory` to manage `HttpClient` instances, allowing consumers to configure it as needed.
- The API key and base URL should be configurable through a strongly-typed options class that binds to the appropriate section in `appsettings.json`.
- This is configured using the already provided `NotificationLibraryOptions.cs`.

# User Story 2 — Resilience and Retries
As a User i want the Notification Service to automatically retry failed requests with an exponential backoff strategy. I have observed that the API sometimes returns transient failures, and I want to ensure that my application can handle these gracefully without having to implement retry logic myself. I want to be able to configure the number of retry attempts and the base delay for the backoff strategy, I also want to be able to turn this feature on or off.

-- Developer Notes:
- The library should implement retry logic using a library like Polly, allowing for configurable retry attempts and backoff strategy.
- The retry behavior should be configurable through the setup method, with options to enable/disable retries and to specify parameters like retry count and base delay.

# User Story 3 — Notification Type Implementations
As a User i want the Notification Service to provide different implementations for different types of notifications (e.g., email, SMS, push). I want to be able to inject the appropriate implementation based on the type of notification I am sending. For example, if I am sending an email notification, I want to inject an `IEmailNotificationService` that has methods specific to email notifications. I want the library to provide a common interface for sending notifications, but also allow for type-specific methods and properties. I also want the specific implementation to handle any nuances related to that notification type, such as required fields or formatting. I Also want to be able to turn on or off specific notification types based on my application's needs. By default, only the base `INotificationService` should be registered, and specific implementations should be opt-in.

-- Developer Notes:
- The library should define a common interface (e.g., `INotificationService`) for sending notifications, as well as type-specific interfaces (e.g., `IEmailNotificationService`, `ISmsNotificationService`).
- The library should provide implementations for each notification type, and the DI setup should allow consumers to inject the appropriate implementation based on their needs.
- The library should allow consumers to enable or disable specific notification types through `NotificationLibraryOptions`.

# User Story 4 — Queueing and Background Processing
As a User i want the Notification Service to support queueing notifications for background processing. I have scenarios where I want to send notifications asynchronously, without blocking the main thread of my application. I want the library to provide a way to enqueue notifications, which can then be processed by a background worker or hosted service. I also want to be able to configure the queueing mechanism, such as using an in-memory queue for simple scenarios or integrating with a message broker like RabbitMQ or Azure Service Bus for more complex setups. Additionally, I want the library to handle retries and failures for queued notifications, ensuring that they are eventually sent even if there are transient issues.

-- Developer Notes:
- The library should provide a mechanism for enqueuing notifications, which can be processed by a background worker or hosted service.
- The queueing mechanism should be configurable, allowing for different implementations such as in-memory queues or message brokers like RabbitMQ or Azure Service Bus.
- The library should handle retries and failures for queued notifications, ensuring that they are eventually sent even if there are transient issues.

# User Story 5 — Multi-tenancy and API Keys
As a User i want the Notification Service to support multi-tenancy by allowing me to specify the API key on a per-request basis. I have multiple tenants using the same application, and each tenant has its own API key for the Notification Service. I want to be able to specify the API key when sending a notification, rather than having it fixed at startup. This way, I can ensure that notifications are sent using the correct credentials for each tenant. I also want to be able to fall back to a default API key if none is specified for a particular request.

-- Developer Notes:
- The library should allow consumers to specify the API key on a per-request basis, perhaps through an overload of the send method or through a context object.
- The library should also support a default API key that can be configured at startup, which will be used if no per-request key is provided.
- This requirement may affect the design of the `INotificationService` interface and how the API key is passed through to the underlying HTTP client.