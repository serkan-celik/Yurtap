import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { YoklamaDetayComponent } from './yoklama-detay.component';

describe('YoklamaDetayComponent', () => {
  let component: YoklamaDetayComponent;
  let fixture: ComponentFixture<YoklamaDetayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ YoklamaDetayComponent ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(YoklamaDetayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
