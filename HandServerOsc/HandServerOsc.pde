import processing.serial.*;

import oscP5.*;
import netP5.*;

OscP5 oscP5;
NetAddress myRemoteLocation;


Serial myPort;

float rx, ry, rz, resetx, resety, resetz;
float []fingers=new float[5];
void setup() {

  size(400, 400);

  printArray( Serial.list() ); 
  myPort = new Serial( this, "COM3", 115200 );
  myPort.clear();


  oscP5 = new OscP5(this, 9000);

  myRemoteLocation = new NetAddress("127.0.0.1", 8000);
}

void draw() {
  background(0);
  while ( myPort.available () > 0 ) {
    String data = myPort.readStringUntil( '\n' );
    if ( data != null ) {
      println( data );
      String[] tmp = splitTokens(data, "\t");
      println(tmp.length);
      if (tmp.length == 5) {
        println("test");
        rx = parseFloat(tmp[1]);
        ry = parseFloat(tmp[2]);
        rz = parseFloat(tmp[3]);
        fingers[0]=parseFloat(tmp[4]);
        //fingers[1]=parseFloat(tmp[5]);
        //fingers[2]=parseFloat(tmp[6]);
        //fingers[3]=parseFloat(tmp[7]);
        //fingers[4]=parseFloat(tmp[8]);
        if (millis() > 20000 && millis() <30000) {
          text("Initialisation : ok", 10, height-10);
          resetx= rx;
          resety=ry;
          resetz=rz;
        }
      }
    }
  }

  text("rotation selon axe x: " + rx, 10, 10);
  text("rotation selon axe y: " + ry, 10, 60);
  text("rotation selon axe z: " + rz, 10, 110);
  text("Finger 1 angle: " + fingers[0], 10, 160);
  //text("Finger 2 angle: " + fingers[1], 10, 210);
  //text("Finger 3 angle: " + fingers[2], 10, 260);
  //text("Finger 4 angle: " + fingers[3], 10, 310);
  //text("Finger 5 angle: " + fingers[4], 10, 360);
  
 // text("valeur reset axe x : " + (rx-resetx), 10, 30);
 // text("valeur reset  axe y : " + (ry-resety), 10, 80);
 // text("valeur reset  axe z : " + (rz-resetz), 10, 130);


  OscMessage myMessage = new OscMessage("/mpu6050");
  //send information about arm rotate angel
  myMessage.add(rx-resetx); 
  myMessage.add(ry-resety); 
  myMessage.add(rz-resetz); 
  //send information about finger angel
  myMessage.add(fingers[0]);
  //myMessage.add(fingers[1]);
  //myMessage.add(fingers[2]);
  //myMessage.add(fingers[3]);
  //myMessage.add(fingers[4]);
      
  oscP5.send(myMessage, myRemoteLocation);
}