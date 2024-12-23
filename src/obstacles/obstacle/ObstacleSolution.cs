
namespace FallingDummy.src.obstacles.obstacle
{
    public enum ObstacleSolution
    {
        NOTHING,          // No action
        TAP,              // One tap on the screen
        DOUBLE_TAP,       // Double presses on the screen
        SWIPE_X,          // Swipe LEFT or RIGHT
        SWIPE_LEFT,       // Swipe LEFT
        SWIPE_RIGHT,      // Swipe RIGHT
        SWIPE_Y,          // Swipe UP or DOWN
        SWIPE_UP,         // Swipe UP
        SWIPE_DOWN,       // Swipe DOWN
        LONG_PRESS,       // A prolonged press on the screen
        PINCH_IN,         // Pinching two fingers together
        PINCH_OUT,        // Spreading two fingers apart
        ROTATE_CLOCKWISE, // Rotating fingers in a clockwise circle
        ROTATE_COUNTER,   // Rotating fingers counter-clockwise
        SHAKE,            // Shaking the device
        TILT_LEFT,        // Tilting the device left
        TILT_RIGHT,       // Tilting the device right
        MULTI_TOUCH,      // Touching the screen with multiple fingers simultaneously
    }
}
