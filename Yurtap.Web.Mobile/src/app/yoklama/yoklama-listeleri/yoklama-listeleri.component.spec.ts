import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { YoklamaListeleriComponent } from './yoklama-listeleri.component';

describe('YoklamalarComponent', () => {
  let component: YoklamaListeleriComponent;
  let fixture: ComponentFixture<YoklamaListeleriComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ YoklamaListeleriComponent ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(YoklamaListeleriComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
