# Async Process Demo

The purpose of this app to to show one way to asynchronously process messages from your client and broadcast the resulting domain changes out to all clients.

## Getting Started

To run this app, you must have Docker installed and running.

### Starting the Backend

To start the backend, simply open `src/server/AsyncDemo.sln`, set the docker-compose project as your start-up project, and Start Debugging.

## Starting the Front-end

Assuming you have Node.js installed, open a Command Line Terminal in `src/client`, and run `npm start`