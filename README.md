# basic_imageprocessing_parallel 240-441 MULTI-CORE PRO & ARCHITECTURE

# หลักการทำงาน
โปรเกรมจะรับรูปภาพเข้าสู่โปรแกรมแล้วจากนั้นก็จะดูในทุกๆ pixel ว่ามีสีอะไรจาก getpixel จากนั้นก็จะใช้ setpixle ในการเปลี่ยนสี pixel นั้นๆให้เป็นไปตามที่เราต้องการ (แดง,น้ำเงิน,เขียว) สีที่เราไมต้องการก็จะกลายเป็น 0 (สีดำ) ผลลัพท์ที่ได้จากวิธีการนี้ก็จะเป็นการดึงสีที่ต้องการมาแสดงผลเท่านั้น เช่น extract red รูปผลลัพท์ก็จะเป็นสีแดงและสีดำ ไม่มีสีอื่นในรูปเลย (เป็นแบบ serial)
ซึ่งตัวโปรแกรมจะมีแบบอื่นด้วยคือแบบ lockbit คือจะทำการ lock บิทในภาพนั้นไว้แล้วทำการเปลี่ยนสีในระดับ byte ซึ่งสิ่งที่จะได้คือจะมีเวลาในการทำงานน้อยกว่าแบบบน อีกทั้งแบบ lockbit ยังสามารถทำงานแบบ parallel ได้ด้วยเนื่องจากไม่ได้ไปล๊อคหน่วยความจำในการเปลี่ยนสีนั้นเอง

# ผลลัพท์ที่ได้
- Load image
 ![Screenshot 2023-03-24 191803](https://user-images.githubusercontent.com/89448778/227519974-0631db99-8343-417a-8c9f-63fea5628d4a.png)
- Extract Red
 ![Screenshot 2023-03-24 191825](https://user-images.githubusercontent.com/89448778/227520013-36bc4760-c270-43f0-ac4e-114fa99c2d5f.png)
- Extract Red (lockbit)
 ![Screenshot 2023-03-24 191843](https://user-images.githubusercontent.com/89448778/227520094-f78425a6-da96-4ae9-85d5-1ed05e02e3c7.png)
- Extract Red (lockbit + parallel)
 ![Screenshot 2023-03-24 192002](https://user-images.githubusercontent.com/89448778/227520102-81367112-9013-4b40-8d5a-58711c81cca3.png)
- Extract Blue
 ![Screenshot 2023-03-24 191909](https://user-images.githubusercontent.com/89448778/227520127-a6abebe7-59f1-4a76-bded-643e166c0bdc.png)
- Extract Blue (lockbit)
 ![Screenshot 2023-03-24 191921](https://user-images.githubusercontent.com/89448778/227520143-5b7c629e-693d-4f23-886f-5f9a133d5ace.png)
- Extract Blue (lockbit + parallel)
 ![Screenshot 2023-03-24 192015](https://user-images.githubusercontent.com/89448778/227520152-1d923813-f6e3-4e49-869c-9cb7f02e30a6.png)
- Extract Green
 ![Screenshot 2023-03-24 191933](https://user-images.githubusercontent.com/89448778/227520168-c0d01ff1-39b5-4900-bf67-63d4cc1a52ee.png)
- Extract Green (lockbit)
 ![Screenshot 2023-03-24 191944](https://user-images.githubusercontent.com/89448778/227520179-c16231e6-a055-46bf-ac0c-c4332ffedfbd.png)
- Extract Green (lockbit + parallel)
![Screenshot 2023-03-24 192027](https://user-images.githubusercontent.com/89448778/227520196-f1ae2172-2cd2-4755-87fe-2323e31829d0.png)
