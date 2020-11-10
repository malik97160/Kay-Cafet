import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

@Injectable()
export class StorageService {
  private storage: any;

  constructor() {
    this.storage = localStorage;
   }

   public retrieve(key: string): any{
      let item = this.storage.getItem(key);

      if (item && item != 'undefined'){
        return JSON.parse(item);
      }
   }

   public store(key: string, value: any){
     this.storage.setItem(key, JSON.stringify(value));
   }

   public clear(key: string){
     this.storage.removeItem(key);
   }
}
