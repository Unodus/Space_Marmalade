//
//  AudioMobVolumePlugin.h
//  AudioMobAudio
//
//  Created by Manjit Bedi on 2020-05-11.
//  Copyright © 2020 AudioMob. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <AVFoundation/AVFoundation.h>
#import <MediaPlayer/MediaPlayer.h>

#ifndef AudioMobVolumePlugin_h
#define AudioMobVolumePlugin_h

extern NSString * const AUMVolumeChangedNotification;
extern NSString * const AUMSilentModeChangedNotification;

extern NSString * const AUMVolumeKey;

@interface AUMVolumePlugin: NSObject

/**
   Class method, get the current system volume.
*/
+ (float)getSystemAudioVolume;

/**
    Set the system volume on a device. This employs a hack as there is no  public Apple API.
 */
+ (void)setSystemVolume:(float) volume;

/**
    Set audio to play using the select AV session category. e.g.:

        AVAudioSessionCategoryAmbient, respects silent mode
        AVAudioSessionCategoryPlayback, silent mode is ignored
*/
+ (void)audioFocusOn:(AVAudioSessionCategory) category;

/**
    This method would get called when ad audio has finished playing.
 */
+ (void)audioFocusOff;


@end


#endif /* AudioMobVolumePlugin_h */
