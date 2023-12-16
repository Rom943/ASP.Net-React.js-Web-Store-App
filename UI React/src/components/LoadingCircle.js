<<<<<<< HEAD
import React, { useEffect, useRef } from 'react';

const LoadingCircle = ({ radius = 100, strokeWidth = 8, color = '#3498db', text = 'Loading...' }) => {
  const canvasRef = useRef(null);

  useEffect(() => {
    const canvas = canvasRef.current;
    const context = canvas.getContext('2d');

    // Function to draw the loading circle
    const draw = (currentProgress) => {
      const centerX = canvas.width / 2;
      const centerY = canvas.height / 2;
      const startAngle = 0;
      const endAngle = (Math.PI * 2 * currentProgress) || Math.PI * 2;

      // Clear the canvas
      context.clearRect(0, 0, canvas.width, canvas.height);

      // Draw the circle
      context.beginPath();
      context.arc(centerX, centerY, radius, startAngle, endAngle);
      context.lineWidth = strokeWidth;
      context.strokeStyle = color;
      context.stroke();

      // Draw the text in the center
      context.font = '18px Arial';
      context.fillStyle = color;
      context.textAlign = 'center';
      context.textBaseline = 'middle';
      context.fillText(text, centerX, centerY);
    };

    // Animation logic
    let progress = 0;
    const animate = () => {
      draw(progress);
      progress = (progress + 0.01) % 1;
      requestAnimationFrame(animate);
    };

    // Start the animation loop
    animate();
  }, [radius, strokeWidth, color, text]);

  return <div className='d-flex justify-content-center align-items-center'><canvas ref={canvasRef} width={radius * 2.5} height={radius * 2.5} /></div>;
};

export default LoadingCircle;
=======
import React, { useEffect, useRef } from 'react';

const LoadingCircle = ({ radius = 100, strokeWidth = 8, color = '#3498db', text = 'Loading...' }) => {
  const canvasRef = useRef(null);

  useEffect(() => {
    const canvas = canvasRef.current;
    const context = canvas.getContext('2d');

    // Function to draw the loading circle
    const draw = (currentProgress) => {
      const centerX = canvas.width / 2;
      const centerY = canvas.height / 2;
      const startAngle = 0;
      const endAngle = (Math.PI * 2 * currentProgress) || Math.PI * 2;

      // Clear the canvas
      context.clearRect(0, 0, canvas.width, canvas.height);

      // Draw the circle
      context.beginPath();
      context.arc(centerX, centerY, radius, startAngle, endAngle);
      context.lineWidth = strokeWidth;
      context.strokeStyle = color;
      context.stroke();

      // Draw the text in the center
      context.font = '18px Arial';
      context.fillStyle = color;
      context.textAlign = 'center';
      context.textBaseline = 'middle';
      context.fillText(text, centerX, centerY);
    };

    // Animation logic
    let progress = 0;
    const animate = () => {
      draw(progress);
      progress = (progress + 0.01) % 1;
      requestAnimationFrame(animate);
    };

    // Start the animation loop
    animate();
  }, [radius, strokeWidth, color, text]);

  return <div className='d-flex justify-content-center align-items-center'><canvas ref={canvasRef} width={radius * 2.5} height={radius * 2.5} /></div>;
};

export default LoadingCircle;
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
