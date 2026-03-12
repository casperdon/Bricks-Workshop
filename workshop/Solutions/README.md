# Solutions
This folder contains one possible solution to the workshop exercises. It is organized by user story, so you can see how the design evolves as new requirements are added.

Please refrain from looking at the solutions until after you have completed the exercises on your own. The purpose of the workshop is to challenge you to design and implement the library yourself, and looking at the solutions beforehand may limit your creativity and learning experience.

## Structure
```
Solutions/
  Consumer/                 An example consumer, with implementations for every user story.

  US1/						Solution for User Story 1
    NotificationLibrary.Workshop/		    The library implementation for User Story 1

  US2/						Solution for User Story 2
    NotificationLibrary.Workshop/		    The library implementation for User Story 2
    NotificationLibrary.Workshop.Polly/		A separate project for Polly-based resilience.

  US3/						Solution for User Story 3
    NotificationLibrary.Workshop/		    The library implementation for User Story 3
    NotificationLibrary.Workshop.Polly/		A separate project for Polly-based resilience.
    NotificationLibrary.Workshop.Email/     A separate project for the email notification implementation.
    NotificationLibrary.Workshop.Sms/       A separate project for the SMS notification implementation.

  US4/						Solution for User Story 4
    NotificationLibrary.Workshop/		            The library implementation for User Story 4
    NotificationLibrary.Workshop.Polly/		        A separate project for Polly-based resilience.
    NotificationLibrary.Workshop.Email/             A separate project for the email notification implementation.
    NotificationLibrary.Workshop.Sms/               A separate project for the SMS notification implementation.
    NotificationLibrary.Workshop.Queue/             A separate project for the queueing implementation.
    NotificationLibrary.Workshop.Queue.InMemory/    An in-memory queue implementation.
    NotificationLibrary.Workshop.Queue.RabbitMQ/    A RabbitMQ queue implementation.
```