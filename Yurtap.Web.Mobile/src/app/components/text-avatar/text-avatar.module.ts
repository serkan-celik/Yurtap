import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { ColorGenerator } from './color-generator';
import { TextAvatarDirective } from './text-avatar'

@NgModule({
  imports: [CommonModule],
  declarations: [TextAvatarDirective],
  exports: [TextAvatarDirective],
  providers: [ColorGenerator]
})
export class TextAvatarModule {}