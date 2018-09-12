# Remote_Mouse

This project is an enhancement to the existing motion project by  Andrew Kirillov andrew.kirillov@gmail.com
My contribution would be MotionDetector1.cs in his motion project and using my own Client-Server mechanism and detecting colors of the brightest pixel of the image frame and assigning windows mouse functions according to the color.

The goal of the project :
The purpose of the system is to allow a user to remotely control the mouse functions in multiple computers connected via LAN using image processing to detect signals coming from a moving object (a bright LED). Image frames from the webcam are processed to find the brightest area of pixels against a high contrast background. The central server runs with a static IP and accepts connections from multiple clients. The clients analyze the frames from the webcam and detect the movement of the LEDs of different colors and simulate mouse clicks and dragging using system calls. When the user request for a change of terminal communicated to the server, it wakes up next client in the queue and the present goes to sleep unless woken by the server. The system helps the user to communicate with multiple terminals through signaling and detection of moving bright object without the inconvenience of using multiple mice.
